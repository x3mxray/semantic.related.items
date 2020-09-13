using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Semantic.Foundation.RelatedContentTagging.Models;
using Semantic.Foundation.RelatedContentTagging.Pipelines.NormalizeContent;
using Sitecore.Abstractions;
using Sitecore.ContentTagging.Core.Messaging;
using Sitecore.DependencyInjection;

namespace Semantic.Foundation.RelatedContentTagging.Pipelines.TagContent
{
    public class Normalize
    {
        [NonSerialized]
        protected readonly BaseCorePipelineManager PipelineManager = ServiceLocator.ServiceProvider.GetService<BaseCorePipelineManager>();

        public void Process(RelatedContentTagArgs args)
        {
            NormalizeContentArgs normalizeContentArgs = new NormalizeContentArgs
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