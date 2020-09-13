using System.Text;
using System.Web;
using HtmlAgilityPack;
using Semantic.Foundation.RelatedContentTagging.Models;

namespace Semantic.Foundation.RelatedContentTagging.Pipelines.NormalizeContent
{
    public class StripHtml
    {
        public void Process(NormalizeContentArgs args)
        {
            foreach (RelatedTaggableContent taggableContent in args.Content)
            {
                taggableContent.Content = StripHtmlTags(taggableContent.Content);
            }
        }

        protected virtual string StripHtmlTags(string html)
        {
            if (string.IsNullOrWhiteSpace(html))
                return string.Empty;

            html = html.Trim().ToLower();

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            var documentNode = htmlDocument.DocumentNode;
            if (documentNode?.ChildNodes == null)
                return HttpUtility.HtmlDecode(html);
            var stringBuilder = new StringBuilder();
            foreach (HtmlNode childNode in htmlDocument.DocumentNode.ChildNodes)
            {
                if (!string.IsNullOrWhiteSpace(childNode.InnerText))
                    stringBuilder.Append(childNode.InnerText + " ");
            }
            return HttpUtility.HtmlDecode(stringBuilder.ToString().Trim());
        }
    }
}