using Hackathon.Boilerplate.Foundation.RelatedContentTagging.Pipelines.TagContent;
using Hackathon.Boilerplate.Foundation.RelatedContentTagging.Providers;
using Sitecore.Abstractions;
using Sitecore.ContentTagging.Core.Messaging;
using Sitecore.ContentTagging.Pipelines.GetTaggingConfiguration;
using Sitecore.ContentTagging.Pipelines.TagContent;
using Sitecore.Data.Items;

namespace Hackathon.Boilerplate.Foundation.RelatedContentTagging.Services
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

            GetTaggingConfigurationArgs configurationArgs = new GetTaggingConfigurationArgs
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
                    //DiscoveryProviders = configurationArgs.ProvidersSet.DiscoveryProviders,

                },//(configurationArgs.ProvidersSet),
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