using System;
using System.Collections.Generic;
using System.Linq;
using Hackathon.Boilerplate.Foundation.RelatedContentTagging.Providers;
using Sitecore.ContentTagging.Core.Messaging;
using Sitecore.Diagnostics;

namespace Hackathon.Boilerplate.Foundation.RelatedContentTagging.Pipelines.TagContent
{
    public class GetRelatedItems
    {
        public void Process(RelatedContentTagArgs args)
        {
            var relatedItemsList = new List<Guid>();
            foreach (IRelatedItemsDiscoveryProvider discoveryProvider in args.Configuration.DiscoveryProviders)
            {
                try
                {
                    Sitecore.Data.Fields.MultilistField relatedTemplates = args.ContentItem.Fields[Constants.Fields.RetatedTemplates];
                    var relatedTemplatesIds = relatedTemplates.TargetIDs.Select(x => x.Guid);

                    IEnumerable<Guid> tags = discoveryProvider.GetRelatedItems(args.ContentItem.ID.Guid, relatedTemplatesIds);
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
            }
            args.RelatedItems = relatedItemsList;
        }
    }
}