using Hackathon.Boilerplate.Foundation.RelatedContentTagging.Providers;
using Sitecore.ContentTagging.Core.Messaging;
using Sitecore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hackathon.Boilerplate.Foundation.RelatedContentTagging.Pipelines.TagContent
{
    public class GetRelatedItems
    {
        public void Process(RelatedContentTagArgs args)
        {
            var relatedItemsList = new List<Guid>();
            IRelatedItemsDiscoveryProvider discoveryProvider = args.Configuration.DiscoveryProvider;
            try
            {
                Sitecore.Data.Fields.MultilistField relatedTemplates = args.ContentItem.Fields[Constants.Fields.RetatedTemplates];
                var relatedTemplatesIds = relatedTemplates.TargetIDs.Select(x => x.Guid);

                IEnumerable<Guid> tags = discoveryProvider.GetRelatedItems(args.ContentItem.ID.Guid, relatedTemplatesIds);

                if (tags == null || !tags.Any())
                {
                    var message = $"No related items find for {args.ContentItem.Name}";
                    MessageBus messageBus = args.MessageBus;
                    messageBus?.SendMessage(new Message
                    {
                        Body = message,
                        Level = MessageLevel.Info
                    });
                    Log.Warn(message, this);
                    args.AbortPipeline();
                }
                else 
                    relatedItemsList.AddRange(tags);
            }
            catch (Exception ex)
            {

                string message = "An error occured in " + discoveryProvider.GetType().Name + " provider";
                MessageBus messageBus = args.MessageBus;
                messageBus?.SendMessage(new Message
                {
                    Body = message,
                    Level = MessageLevel.Error
                });
                Log.Error(message, ex, this);
            }
            args.RelatedItems = relatedItemsList;
        }
    }
}