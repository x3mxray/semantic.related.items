using Sitecore.ContentTagging.Core.Messaging;
using Sitecore.Pipelines;

namespace Semantic.Foundation.RelatedContentTagging.Providers
{

    public class GetRelatedContentTaggingConfigurationArgs : PipelineArgs
    {
        /// <summary>Configuration name</summary>
        public string ConfigurationName { get; set; }

        /// <summary>Preserves tagging configuration.</summary>
        public RelatedItemContentTaggingProvidersSet ProvidersSet { get; set; }

        /// <summary>Message bus</summary>
        public MessageBus MessageBus { get; set; }
    }
}