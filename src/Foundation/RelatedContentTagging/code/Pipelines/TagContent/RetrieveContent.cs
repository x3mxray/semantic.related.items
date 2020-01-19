using Sitecore.ContentTagging.Core.Models;
using Sitecore.ContentTagging.Core.Providers;
using Sitecore.Data.Items;
using System.Collections.Generic;

namespace Hackathon.Boilerplate.Foundation.RelatedContentTagging.Pipelines.TagContent
{
    public class RetrieveContent
    {
        public void Process(RelatedContentTagArgs args)
        {
            List<TaggableContent> taggableContentList = new List<TaggableContent>();
            foreach (IContentProvider<Item> contentProvider in args.Configuration.ContentProviders)
            {
                TaggableContent content = contentProvider.GetContent(args.ContentItem);
                taggableContentList.Add(content);
            }
            args.Content = taggableContentList;
        }
    }
}