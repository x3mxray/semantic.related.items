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

                if (string.IsNullOrEmpty(content) || string.IsNullOrWhiteSpace(content))
                {
                    MessageBus messageBus = args.MessageBus;
                    if (messageBus != null)
                        messageBus.SendMessage(new Message
                        {
                            Body = $"Item {args.ContentItem.Name} contains no content.",
                            Level = MessageLevel.Warning
                        });
                    args.AbortPipeline();
                }

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