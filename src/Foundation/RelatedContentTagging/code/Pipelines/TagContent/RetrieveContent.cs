using Sitecore.ContentTagging.Core.Models;
using Sitecore.ContentTagging.Core.Providers;
using Sitecore.Data.Items;
using System.Collections.Generic;
using Hackathon.Boilerplate.Foundation.RelatedContentTagging.Models;

namespace Hackathon.Boilerplate.Foundation.RelatedContentTagging.Pipelines.TagContent
{
    public class RetrieveContent
    {
        public void Process(RelatedContentTagArgs args)
        {
            var taggableContentList = new List<RelatedTaggableContent>();
            foreach (IContentProvider<Item> contentProvider in args.Configuration.ContentProviders)
            {
                var content = (StringContent) contentProvider.GetContent(args.ContentItem);

                if (!string.IsNullOrEmpty(content.Content) && !string.IsNullOrEmpty(content.Content.Trim()))
                {
                    taggableContentList.Add(new RelatedTaggableContent
                    {
                        Content = content.Content
                    });
                }
            }
            args.Content = taggableContentList;
        }
    }
}