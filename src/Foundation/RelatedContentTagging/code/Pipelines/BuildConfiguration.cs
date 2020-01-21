using Hackathon.Boilerplate.Foundation.RelatedContentTagging.Providers;
using Hackathon.Boilerplate.Foundation.RelatedContentTagging.Services;

namespace Hackathon.Boilerplate.Foundation.RelatedContentTagging.Pipelines
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