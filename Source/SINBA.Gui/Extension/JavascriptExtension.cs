using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace Sinba.Gui.Extension
{
    /// <summary>
    /// Extension Class including extension methods for JavaScript and CSS files.
    /// </summary>
    public static class JavascriptExtension
    {
        #region Versioned Content

        /// <summary>
        /// Includes the versioned js.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="filename">The filename.</param>
        /// <returns>A versioned JavaScript file.</returns>
        public static MvcHtmlString IncludeVersionedJs(this HtmlHelper helper, string filename)
        {
            string version = GetVersion(helper, filename);
            return MvcHtmlString.Create("<script type='text/javascript' src='" + filename + version + "'></script>");
        }

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="filename">The filename.</param>
        /// <returns>The version of the file.</returns>
        private static string GetVersion(this HtmlHelper helper, string filename)
        {
            var context = helper.ViewContext.RequestContext.HttpContext;

            if (context.Cache[filename] == null)
            {
                var physicalPath = context.Server.MapPath(filename);
                var version = "?v=" +
                  new System.IO.FileInfo(physicalPath).LastWriteTime
                    .ToString("yyyyMMddHHmmss");
                context.Cache.Add(physicalPath, version, null,
                  DateTime.Now.AddMinutes(1), TimeSpan.Zero,
                  CacheItemPriority.Normal, null);
                context.Cache[filename] = version;
                return version;
            }
            else
            {
                return context.Cache[filename] as string;
            }
        }

        /// <summary>
        /// Gets a versioned content of the file.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="contentPath">The content path.</param>
        /// <returns>A versioned file path.</returns>
        public static string VersionedContent(this UrlHelper helper, string contentPath)
        {
            var context = helper.RequestContext.HttpContext;
            var translatedContentPath = helper.Content(contentPath);

            string versionedContentPath;
            var physicalPath = context.Server.MapPath(contentPath);
            var version = @"v=" + new System.IO.FileInfo(physicalPath).LastWriteTime.ToString(@"yyyyMMddHHmmss");

            versionedContentPath =
                contentPath.Contains(@"?")
                    ? translatedContentPath + @"&" + version
                    : translatedContentPath + @"?" + version;

            if (context.Cache[contentPath] == null)
            {
                SetContextCache(context, physicalPath, version, contentPath, versionedContentPath);
                return versionedContentPath;
            }
            else
            {
                var cachedValue = context.Cache[contentPath] as string;

                if (cachedValue.Equals(versionedContentPath))
                {
                    return cachedValue;
                }

                SetContextCache(context, physicalPath, version, contentPath, versionedContentPath);
                return versionedContentPath;
            }
        }

        /// <summary>
        /// Sets the context cache.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="contentPath">The content path.</param>
        /// <param name="versionedContentPath">The versioned content path.</param>
        private static void SetContextCache(HttpContextBase context, string key, object value, string contentPath, string versionedContentPath)
        {
            context.Cache.Add(key, value, null, DateTime.Now.AddMinutes(1), TimeSpan.Zero, CacheItemPriority.Normal, null);
            context.Cache[contentPath] = versionedContentPath;
        }
        #endregion
    }
}