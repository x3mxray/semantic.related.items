using Sitecore.ContentTagging.Core.Configuration;
using Sitecore.ContentTagging.Core.Factories;

namespace Hackathon.Boilerplate.Foundation.RelatedContentTagging.Providers
{
    public class RelatedContentTaggingProviderFactory : ContentTaggingProviderFactory, IRelatedContentTaggingProviderFactory
    {
        public RelatedContentTaggingProviderFactory(IContentTaggingProvidersConfigurationService contentTaggingProvidersConfigurationService) : base(contentTaggingProvidersConfigurationService)
        {
        }

        public IRelatedItemsDiscoveryProvider CreateRelatedItemsDiscoveryProvider(string providerName)
        {
            return this.GetProvider<IRelatedItemsDiscoveryProvider>(providerName);
        }
    }

    public interface IRelatedContentTaggingProviderFactory : IContentTaggingProviderFactory
    {
        IRelatedItemsDiscoveryProvider CreateRelatedItemsDiscoveryProvider(string providerName);
    }
}