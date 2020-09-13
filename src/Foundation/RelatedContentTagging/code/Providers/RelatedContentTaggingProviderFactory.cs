using System;
using Semantic.Foundation.RelatedContentTagging.Services;
using Sitecore.ContentTagging.Core.Providers;
using Sitecore.Reflection;

namespace Semantic.Foundation.RelatedContentTagging.Providers
{
    public class RelatedContentTaggingProviderFactory :  IRelatedContentTaggingProviderFactory
    {
        protected IRelatedContentTaggingProvidersConfigurationService ContentTaggingProvidersConfigurationService;

        public RelatedContentTaggingProviderFactory(IRelatedContentTaggingProvidersConfigurationService configurationService)
        {
            this.ContentTaggingProvidersConfigurationService = configurationService;
        }

        public IContentProvider<T> CreateContentProvider<T>(string providerName)
        {
            return this.GetProvider<IContentProvider<T>>(providerName);
        }

        public IRelatedItemsDiscoveryProvider CreateDiscoveryProvider(string providerName)
        {
            return GetProvider<IRelatedItemsDiscoveryProvider>(providerName);
        }


        public IRelatedItemsTagger<T> CreateTagger<T>(string providerName)
        {
            return this.GetProvider<IRelatedItemsTagger<T>>(providerName);
        }

        protected virtual T GetProvider<T>(string providerName)
        {
            Type providerTypeByName = this.ContentTaggingProvidersConfigurationService.GetProviderTypeByName(providerName);
            if (providerTypeByName != (Type)null)
                return (T)ReflectionUtil.CreateObject(providerTypeByName);
            return default(T);
        }
    }

    public interface IRelatedContentTaggingProviderFactory
    {
        IContentProvider<T> CreateContentProvider<T>(string providerName);

        IRelatedItemsDiscoveryProvider CreateDiscoveryProvider(string providerName);

        IRelatedItemsTagger<T> CreateTagger<T>(string providerName);
    }
}