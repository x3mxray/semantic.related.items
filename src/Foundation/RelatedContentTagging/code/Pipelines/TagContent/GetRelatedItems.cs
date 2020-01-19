using System;
using System.Collections.Generic;
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
                    IEnumerable<Guid> tags = discoveryProvider.GetRelatedItems(args.Content);
                    relatedItemsList.AddRange(tags);
                }
                catch (Exception ex)
                {
                    
                    string message = "An error occured in " + discoveryProvider.GetType().Name + " provider";
                    MessageBus messageBus = args.MessageBus;
                    if (messageBus != null)
                        messageBus.SendMessage(new Message
                        {
                            Body = message,
                            Level = MessageLevel.Error
                        });
                    Log.Error(message, ex, (object)this);
                }
            }
            args.Tags = relatedItemsList;
        }
    }
}