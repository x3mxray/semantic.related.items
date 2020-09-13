using System.Collections.Generic;
using System.Linq;
using Semantic.Foundation.RelatedContentTagging.Extentions;
using Semantic.Foundation.RelatedContentTagging.Models;
using Sitecore.ContentTagging.Core.Messaging;
using Sitecore.ContentTagging.Core.Models;
using Sitecore.ContentTagging.Core.Providers;
using Sitecore.Data.Items;

namespace Semantic.Foundation.RelatedContentTagging.Pipelines.TagContent
{
    public class RetrieveContentFromDatasourceItems
    {
        public void Process(RelatedContentTagArgs args)
        {
            var taggableContentList = new List<RelatedTaggableContent>();
            if (args.Content != null && args.Content.Any())
            {
                taggableContentList.AddRange(args.Content);
            }
            foreach (IContentProvider<Item> contentProvider in args.Configuration.ContentProviders)
            {
                var dataSoutceItems = args.ContentItem.GetDataSourceItems();
                foreach (var item in dataSoutceItems)
                {
                    var content = (StringContent)contentProvider.GetContent(item);

                    if (!string.IsNullOrEmpty(content.Content) && !string.IsNullOrEmpty(content.Content.Trim()))
                    {
                        taggableContentList.Add(new RelatedTaggableContent
                        {
                            Content = content.Content
                        });
                    }
                }
            }

            if (taggableContentList.Count == 0)
            {
                MessageBus messageBus = args.MessageBus;
                if (messageBus != null)
                    messageBus.SendMessage(new Message
                    {
                        Body = $"Item {args.ContentItem.Name} contains no content.",
                        Level = MessageLevel.Info
                    });
                args.AbortPipeline();
            }
            else
                args.Content = taggableContentList;
        }
    }
}