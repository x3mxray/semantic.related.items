using Hackathon.Boilerplate.Foundation.RelatedContentTagging.Models;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.Abstractions;
using Sitecore.ContentTagging.Core.Models;
using Sitecore.ContentTagging.Pipelines.NormalizeContent;
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
            NormalizeContentArgs normalizeContentArgs = new NormalizeContentArgs()
            {
                Content = args.Content,
                MessageBus = args.MessageBus
            };
            this.PipelineManager.Run("normalizeContent", normalizeContentArgs, "ContentTagging");


            var list = new List<RelatedTaggableContent>();
            foreach (RelatedTaggableContent content in normalizeContentArgs.Content)
            {
                list.Add(new RelatedTaggableContent { Content = content.Content });
            }

            args.Content = list;
        }
    }
}