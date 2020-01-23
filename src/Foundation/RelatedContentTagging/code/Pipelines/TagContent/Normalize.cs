using Hackathon.Boilerplate.Foundation.RelatedContentTagging.Models;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.Abstractions;
using Sitecore.DependencyInjection;
using System;
using System.Collections.Generic;

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

            args.Content = list;
        }
    }
}