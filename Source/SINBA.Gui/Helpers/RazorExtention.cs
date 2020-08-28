using Sinba.Gui.UIModels;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Sinba.Gui.Helpers
{
    public static class RazorExtention
    {
        /// <summary>
        /// Creates a label with asterisk for required fields.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="html">The HTML.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="generatedId">if set to <c>true</c> [generated identifier].</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static MvcHtmlString LabelForRequired<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string id = "", bool generatedId = false)
        {
            return LabelHelper(html, ModelMetadata.FromLambdaExpression(expression, html.ViewData), ExpressionHelper.GetExpressionText(expression), id, generatedId);
        }
        private const string RequiredSuffix = " *";
        
        /// <summary>
        /// Creates a label of type HtmlString.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="metadata">The metadata.</param>
        /// <param name="htmlFieldName">Name of the HTML field.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="generatedId">if set to <c>true</c> [generated identifier].</param>
        /// <returns></returns>
        internal static MvcHtmlString LabelHelper(HtmlHelper html, ModelMetadata metadata, string htmlFieldName, string id, bool generatedId)
        {
            string labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            if (String.IsNullOrEmpty(labelText))
            {
                return MvcHtmlString.Empty;
            }
            var sb = new StringBuilder();
            sb.Append(labelText);
            //if (metadata.IsRequired)
            sb.Append(RequiredSuffix);

            var tag = new TagBuilder("label");
            if (!string.IsNullOrWhiteSpace(id))
            {
                tag.Attributes.Add("id", id);
            }
            else if (generatedId)
            {
                tag.Attributes.Add("id", html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName) + "_Label");
            }

            tag.Attributes.Add("for", html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName));
            tag.SetInnerText(sb.ToString());

            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        }

        /// <summary>
        /// Emses the error validation sumary.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="excludePropertyErrors">if set to <c>true</c> [exclude property errors].</param>
        /// <returns></returns>
        public static MvcHtmlString SinbaValidationSumary(this HtmlHelper htmlHelper, bool excludePropertyErrors = true)
        {
            var tag = new TagBuilder("div");
            tag.AddCssClass(CssClasses.ValidationSummary);
            var validationSummary = htmlHelper.ValidationSummary(excludePropertyErrors, string.Empty, new { @class = CssClasses.TextDanger});
            if(validationSummary != null)
            {
                tag.InnerHtml = validationSummary.ToString();
            }
            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        }

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public static string GetPropertyName<TModel, TProperty>(
                this HtmlHelper<TModel> htmlHelper,
                Expression<Func<TModel, TProperty>> expression
            )
        {
            var metaData = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData);
            string value = metaData.PropertyName ?? (metaData.DisplayName ?? ExpressionHelper.GetExpressionText(expression));
            return value;
        }

        /// <summary>
        /// Gets the display name of the property.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public static string GetPropertyDisplayName<TModel, TProperty>(
                this HtmlHelper<TModel> htmlHelper,
                Expression<Func<TModel, TProperty>> expression
            )
        {
            var metaData = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData);
            string value = metaData.DisplayName ?? string.Empty;
            return value;
        }
    }
}