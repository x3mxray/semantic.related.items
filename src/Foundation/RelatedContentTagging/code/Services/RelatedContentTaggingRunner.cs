using Semantic.Foundation.RelatedContentTagging.Pipelines.TagContent;
using Semantic.Foundation.RelatedContentTagging.Providers;
using Sitecore.Abstractions;
using Sitecore.ContentTagging.Core.Messaging;
using Sitecore.Data.Items;

namespace Semantic.Foundation.RelatedContentTagging.Services
{
    public class RelatedContentTaggingRunner : IRelatedContentTaggingRunner
    {
        protected readonly BaseCorePipelineManager PipelineManager;

        public RelatedContentTaggingRunner(BaseCorePipelineManager pipelineManager)
        {
            this.PipelineManager = pipelineManager;
        }

        public void Run(Item contentItem, MessageBus messageBus)
        {
            string pipelineDomain = "RealtedContentTagging";

            var configurationArgs = new GetRelatedContentTaggingConfigurationArgs
            {
                MessageBus = messageBus
            };
            PipelineManager.Run("getRelatedTaggingConfiguration", configurationArgs, pipelineDomain);
            BaseCorePipelineManager pipelineManager = PipelineManager;
            string pipelineName = "tagContent";
            RelatedContentTagArgs tagContentArgs = new RelatedContentTagArgs
            {
                Configuration = new RelatedItemContentTaggingProvidersSet
                {
                    ContentProviders = configurationArgs.ProvidersSet.ContentProviders,
                    Taggers = configurationArgs.ProvidersSet.Taggers,
                    DiscoveryProvider = configurationArgs.ProvidersSet.DiscoveryProvider,

                },
                ContentItem = contentItem,
                MessageBus = messageBus
            };
           
            pipelineManager.Run(pipelineName, tagContentArgs, pipelineDomain);
        }
    }

    public interface IRelatedContentTaggingRunner
    {
        void Run(Item contentItem, MessageBus messageBus = null);
    }
}