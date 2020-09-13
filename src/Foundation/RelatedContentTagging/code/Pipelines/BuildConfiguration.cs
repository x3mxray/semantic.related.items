using Semantic.Foundation.RelatedContentTagging.Providers;
using Semantic.Foundation.RelatedContentTagging.Services;

namespace Semantic.Foundation.RelatedContentTagging.Pipelines
{
    public class BuildConfiguration
    {
        protected IRelatedItemContentTaggingProviderSetBuilder TaggingProviderSetBuilder;

        public BuildConfiguration(IRelatedItemContentTaggingProviderSetBuilder taggingProviderSetBuilder)
        {
            TaggingProviderSetBuilder = taggingProviderSetBuilder;
        }

        public void Process(GetRelatedContentTaggingConfigurationArgs args)
        {
            args.ProvidersSet = TaggingProviderSetBuilder.Build(args.ConfigurationName);
        }
    }
}