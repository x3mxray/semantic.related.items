using Hackathon.Boilerplate.Foundation.RelatedContentTagging.Providers;
using Sitecore.ContentTagging.Core.Messaging;
using Sitecore.Diagnostics;
using System;
using System.Linq;

namespace Hackathon.Boilerplate.Foundation.RelatedContentTagging.Pipelines.TagContent
{
    public class Vectorize
    {
        public void Process(RelatedContentTagArgs args)
        {
            IRelatedItemsDiscoveryProvider discoveryProvider = args.Configuration.DiscoveryProvider;
            try
            {
                var content = string.Join(" ", args.Content.Select(x => x.Content));
                args.Vector = discoveryProvider.GetVector(content);
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