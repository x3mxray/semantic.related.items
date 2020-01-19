using Sitecore.ContentTagging.Configuration;
using Sitecore.ContentTagging.Pipelines.GetTaggingConfiguration;

namespace Hackathon.Boilerplate.Foundation.RelatedContentTagging.Pipelines
{
    public class BuildConfiguration
    {
        /// <summary>Item Content Tagging ProviderSet Builder</summary>
        protected IItemContentTaggingProviderSetBuilder TaggingProviderSetBuilder;

        /// <summary>Constructor</summary>
        /// <param name="taggingProviderSetBuilder">Item Tagging ProviderSet Builder</param>
        public BuildConfiguration(IItemContentTaggingProviderSetBuilder taggingProviderSetBuilder)
        {
            this.TaggingProviderSetBuilder = taggingProviderSetBuilder;
        }

        /// <summary>Pipeline processor entry point</summary>
        /// <param name="args"></param>
        public void Process(GetTaggingConfigurationArgs args)
        {
            args.ProvidersSet = this.TaggingProviderSetBuilder.Build(args.ConfigurationName);
        }
    }
}