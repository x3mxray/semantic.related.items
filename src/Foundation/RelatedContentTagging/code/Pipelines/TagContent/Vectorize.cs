using System;
using Hackathon.Boilerplate.Foundation.RelatedContentTagging.Providers;
using Sitecore.ContentTagging.Core.Messaging;
using Sitecore.Diagnostics;

namespace Hackathon.Boilerplate.Foundation.RelatedContentTagging.Pipelines.TagContent
{
    public class Vectorize
    {
        public void Process(RelatedContentTagArgs args)
        {
            foreach (IRelatedItemsDiscoveryProvider discoveryProvider in args.Configuration.DiscoveryProviders)
            {
                try
                {
                    foreach (var content in args.Content)
                    {
                       content.Vector = discoveryProvider.GetVector(content);
                    }

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
        }
    }
}