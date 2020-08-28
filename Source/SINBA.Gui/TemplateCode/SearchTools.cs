using Sinba.Resources;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;


namespace Sinba.Gui.TemplateCode
{
    /// <summary>
    /// Tools for Navigation search
    /// </summary>
    public static class SearchTools
    {
        #region Constants
        private const string regexAlphaNumericPattern = "[0-9a-zA-Z]";
        #endregion

        #region Variables
        private static bool? isSiteMode;
        private static readonly string[] separators = new string[] { " ", ",", "/", "\\", "-", "+" };
        private static string[] requestExclusions;
        private static string[] prefixes;
        private static string[] postfixes;
        private static string[][] synonyms;
        #endregion

        #region Properties
        /// <summary>
        /// Gets a value indicating whether this instance is site mode.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is site mode; otherwise, <c>false</c>.
        /// </value>
        public static bool IsSiteMode
        {
            get
            {
                if (!isSiteMode.HasValue)
                {
                    isSiteMode = ConfigurationManager.AppSettings[Strings.SiteMode].Equals(Strings.True, StringComparison.InvariantCultureIgnoreCase);
                }
                return isSiteMode.Value;
            }
        }

        /// <summary>
        /// Gets the words exclusions.
        /// </summary>
        /// <value>
        /// The words exclusions.
        /// </value>
        static string[] WordsExclusions
        {
            get
            {
                if (requestExclusions == null)
                    requestExclusions = UserSectionItemsModel.Instance.Search.Exclusions.Words.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                return requestExclusions;
            }
        }
        
        /// <summary>
        /// Gets the prefixes exclusions.
        /// </summary>
        /// <value>
        /// The prefixes exclusions.
        /// </value>
        static string[] PrefixesExclusions
        {
            get
            {
                if (prefixes == null)
                    prefixes = UserSectionItemsModel.Instance.Search.Exclusions.Prefixes.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                return prefixes;
            }
        }
        
        /// <summary>
        /// Gets the postfixes exclusions.
        /// </summary>
        /// <value>
        /// The postfixes exclusions.
        /// </value>
        static string[] PostfixesExclusions
        {
            get
            {
                if (postfixes == null)
                    postfixes = UserSectionItemsModel.Instance.Search.Exclusions.Postfixes.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                return postfixes;
            }
        }
        
        /// <summary>
        /// Gets the synonyms.
        /// </summary>
        /// <value>
        /// The synonyms.
        /// </value>
        static string[][] Synonyms
        {
            get
            {
                if (synonyms == null)
                    synonyms = UserSectionItemsModel.Instance.Search.Synonyms.Groups.Select(s => s.Split(separators, StringSplitOptions.RemoveEmptyEntries)).ToArray();
                return synonyms;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Does the search.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The list of search result.</returns>
        public static List<SearchResult> DoSearch(string request)
        {
            var results = new List<SearchResult>();
            var requests = SplitRequests(request);
            try
            {
                var items = UserSectionItemsModel.GetUserInstance().SortedSectionItems.Where(dp => dp.Visible && !dp.IsRootSection && (dp == UserSectionItemsModel.GetUserCurrent() || (!dp.HideNavItem && IsSiteMode)));
                foreach (var item in items)
                {
                    results.AddRange(DoSearch(requests, item));
                }
            }
            catch { }
            
            results.Sort();
            return results;
        }

        /// <summary>
        /// Gets the keywords rank list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>The rank of keywords.</returns>
        public static Dictionary<string, int> GetKeywordsRankList(ModelBase model)
        {
            List<TextRank> textRanks = new List<TextRank>() { 
                new TextRank(model.Keywords, 3)
            };

            var item = model as SectionItemModel;
            var group = model as SectionGroupModel;
            var section = model as SectionPageModel;

            if (item != null)
            {
                textRanks.Add(new TextRank(item.NavItemTitle, 5));
                textRanks.Add(new TextRank(item.Key, 3));
                textRanks.Add(new TextRank(item.Title, 3));
            }
            else if (group != null)
            {
                textRanks.Add(new TextRank(group.Title, 5));
                textRanks.Add(new TextRank(group.Key, 3));
            }
            else if (section != null)
            {
                textRanks.Add(new TextRank(section.Title, 5));
                textRanks.Add(new TextRank(section.Key, 3));
            }
            return GetKeywordsRankList(textRanks);
        }

        /// <summary>
        /// Calculates the rank.
        /// </summary>
        /// <param name="requests">The requests.</param>
        /// <param name="section">The section.</param>
        /// <returns>The rank.</returns>
        static int CalculateRank(List<string[]> requests, SectionPageModel section)
        {
            int resultRank = 0;
            int keywordRank = 0;
            foreach (var request in requests)
            {
                int requestRank = -1;
                if (CalculateRank(request, section.KeywordsRankList, out keywordRank))
                    requestRank += keywordRank;
                if (section.Group != null && CalculateRank(request, section.Group.KeywordsRankList, out keywordRank))
                    requestRank += keywordRank;
                if (CalculateRank(request, section.Item.KeywordsRankList, out keywordRank))
                    requestRank += keywordRank;
                if (requestRank == -1 && WordsExclusions.Any(re => re.Equals(request[0], StringComparison.InvariantCultureIgnoreCase)))
                    requestRank = 0;

                if (requestRank > -1)
                    resultRank += requestRank;
                else
                    return -1;
            }
            return resultRank;
        }


        /// <summary>
        /// Calculates the rank.
        /// </summary>
        /// <param name="synonyms">The synonyms.</param>
        /// <param name="keywordsRankList">The keywords rank list.</param>
        /// <param name="rank">The rank.</param>
        /// <returns>True if a rank exists.</returns>
        static bool CalculateRank(string[] synonyms, Dictionary<string, int> keywordsRankList, out int rank)
        {
            var keyword = keywordsRankList.Keys.FirstOrDefault(k => MatchWord(synonyms[0], k));
            rank = keyword != null ? keywordsRankList[keyword] : -1;
            if (rank == -1)
            {
                foreach (var syn in synonyms.Skip(1))
                {
                    keyword = keywordsRankList.Keys.FirstOrDefault(k => MatchWord(syn, k));
                    rank += keyword != null ? keywordsRankList[keyword] : -1;
                    if (rank > -1)
                        break;
                }
            }
            return rank > -1;
        }

        /// <summary>
        /// Does the search.
        /// </summary>
        /// <param name="requests">The requests.</param>
        /// <param name="item">The item.</param>
        /// <returns>The list of search results.</returns>
        static IEnumerable<SearchResult> DoSearch(List<string[]> requests, SectionItemModel item)
        {
            var results = new List<SearchResult>();
            foreach (var group in item.Groups)
            {
                if(group.Visible)
                {
                    foreach (var section in group.Sections)
                    {
                        if (section.Visible)
                        {
                            results.AddRange(GetRes(requests, section, string.Format("{0} ({1})", HighlightOccurences(section.Title, requests), HighlightOccurences(section.Group.Title, requests)), section.Item.Title.ToUpper()));
                        }
                    }
                }
            }
            return results;
        }

        /// <summary>
        /// Gets the resource.
        /// </summary>
        /// <param name="requests">The requests.</param>
        /// <param name="section">The section.</param>
        /// <param name="text">The text.</param>
        /// <param name="itemText">The item text.</param>
        /// <returns>The search results.</returns>
        static IEnumerable<SearchResult> GetRes(List<string[]> requests, SectionPageModel section, string text, string itemText)
        {
            var results = new List<SearchResult>();
            var rank = CalculateRank(requests, section);
            if (rank > -1)
            {
                var sr = new SearchResult(section, rank);
                sr.Text = text;
                sr.ItemText = itemText;
                results.Add(sr);
            }
            return results;
        }

        /// <summary>
        /// Highlights the occurences.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="requests">The requests.</param>
        /// <returns>Text to highlight.</returns>
        static string HighlightOccurences(string text, List<string[]> requests)
        {
            var validRequest = new Regex(regexAlphaNumericPattern + "+", RegexOptions.IgnoreCase);
            foreach (var request in requests.SelectMany(r => r))
            {
                if (validRequest.IsMatch(request))
                {
                    Regex re = new Regex("("+ regexAlphaNumericPattern + "*" + request + regexAlphaNumericPattern + "*)", RegexOptions.IgnoreCase);
                    text = re.Replace(text, "<b>$0</b>");
                }
            }
            return text;
        }

        /// <summary>
        /// Splits the requests.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        static List<string[]> SplitRequests(string request)
        {
            var words = request.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            var result = new List<string[]>();
            foreach (var word in words)
            {
                var resultWord = PrepareWord(word);
                var synonymList = Synonyms.FirstOrDefault(list => list.Any(s => MatchWord(resultWord, s)));
                var wordSynonyms = new List<string>() { resultWord };
                if (synonymList != null)
                    wordSynonyms.AddRange(synonymList.Where(s => !MatchWord(resultWord, s)));
                result.Add(wordSynonyms.Distinct().ToArray());
            }
            return result;
        }

        /// <summary>
        /// Prepares the word.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns></returns>
        static string PrepareWord(string word)
        {
            foreach (var prefix in PrefixesExclusions)
            {
                if (word.StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase) && word.Length > prefix.Length)
                    word = word.Remove(0, prefix.Length);
            }
            foreach (var postfix in PostfixesExclusions)
            {
                if (word.EndsWith(postfix, StringComparison.InvariantCultureIgnoreCase) && word.Length > postfix.Length)
                    word = word.Substring(0, word.Length - postfix.Length);
            }
            return word;
        }

        /// <summary>
        /// Matches the word.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="word">The word.</param>
        /// <returns></returns>
        static bool MatchWord(string request, string word)
        {
            return word.IndexOf(request, StringComparison.InvariantCultureIgnoreCase) > -1;
        }

        /// <summary>
        /// Gets the keywords list.
        /// </summary>
        /// <param name="words">The words.</param>
        /// <returns></returns>
        internal static string[] GetKeywordsList(params string[] words)
        {
            return words
                .SelectMany(w => w.Split(separators, StringSplitOptions.RemoveEmptyEntries))
                .Distinct()
                .ToArray();
        }

        /// <summary>
        /// Gets the keywords rank list.
        /// </summary>
        /// <param name="textRanks">The text ranks.</param>
        /// <returns></returns>
        static Dictionary<string, int> GetKeywordsRankList(List<TextRank> textRanks)
        {
            var result = new Dictionary<string, int>();
            foreach (var textRank in textRanks)
            {
                var words = textRank.Text.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in words)
                {
                    var rankWord = PrepareWord(word);
                    if (!result.ContainsKey(rankWord))
                        result[rankWord] = textRank.Rank;
                }
            }
            return result.OrderByDescending(keyValuePair => keyValuePair.Value).ToDictionary(keyValuePair => keyValuePair.Key, keyValuePair => keyValuePair.Value);
        }
        #endregion
    }
}
