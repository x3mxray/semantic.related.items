using Hackathon.Boilerplate.Foundation.RelatedContentTagging.Models;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.Abstractions;
using Sitecore.DependencyInjection;
using System;
using System.Collections.Generic;
using Sitecore.ContentTagging.Core.Messaging;

namespace Hackathon.Boilerplate.Foundation.RelatedContentTagging.Pipelines.TagContent
{
    public class Normalize
    {
        [NonSerialized]
        protected readonly BaseCorePipelineManager PipelineManager = ServiceLocator.ServiceProvider.GetService<BaseCorePipelineManager>();

        public void Process(RelatedContentTagArgs args)
        {
            NormalizeContent.NormalizeContentArgs normalizeContentArgs = new NormalizeContent.NormalizeContentArgs
            {
                Content = args.Content,
                MessageBus = args.MessageBus
            };
            this.PipelineManager.Run("normalizeContent", normalizeContentArgs, "RealtedContentTagging");
            
            var list = new List<RelatedTaggableContent>();
            foreach (RelatedTaggableContent content in normalizeContentArgs.Content)
            {
                list.Add(new RelatedTaggableContent { Content = content.Content });
            }

            if (list.Count == 0)
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
                args.Content = list;
        }
    }
}