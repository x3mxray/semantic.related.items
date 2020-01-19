using System;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.Abstractions;
using Sitecore.ContentTagging.Pipelines.NormalizeContent;
using Sitecore.DependencyInjection;

namespace Hackathon.Boilerplate.Foundation.RelatedContentTagging.Pipelines.TagContent
{
    public class Normalize
    {
        /// <summary>Base pipeline manager</summary>
        [NonSerialized]
        protected readonly BaseCorePipelineManager PipelineManager = ServiceLocator.ServiceProvider.GetService<BaseCorePipelineManager>();

        /// <summary>Apply action</summary>
        /// <param name="args"></param>
        public void Process(RelatedContentTagArgs args)
        {
            NormalizeContentArgs normalizeContentArgs = new NormalizeContentArgs()
            {
                Content = args.Content,
                MessageBus = args.MessageBus
            };
            this.PipelineManager.Run("normalizeContent", normalizeContentArgs, "ContentTagging");
            args.Content = normalizeContentArgs.Content;
        }
    }
}