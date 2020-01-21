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
                TaggableContent content = contentProvider.GetContent(args.ContentItem);
                taggableContentList.Add(new RelatedTaggableContent
                {
                    Content = ((StringContent)content).Content
                });
            }
            args.Content = taggableContentList;
        }
    }
}