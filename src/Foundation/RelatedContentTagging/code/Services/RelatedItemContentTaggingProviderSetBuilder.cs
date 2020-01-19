using System;
using System.Linq;
using Hackathon.Boilerplate.Foundation.RelatedContentTagging.Providers;
using Sitecore.ContentTagging.Configuration;
using Sitecore.ContentTagging.Core.Providers;
using Sitecore.Data.Items;

namespace Hackathon.Boilerplate.Foundation.RelatedContentTagging.Services
{
    public class RelatedItemContentTaggingProviderSetBuilder : IRelatedItemContentTaggingProviderSetBuilder
    {
        protected IRelatedContentTaggingProviderFactory ProvidersFactory;
        protected IRelatedItemContentTaggingConfigurationService ConfigurationService;

        public RelatedItemContentTaggingProviderSetBuilder(IRelatedContentTaggingProviderFactory providersFactory, IRelatedItemContentTaggingConfigurationService configurationService)
        {
            this.ProvidersFactory = providersFactory;
            this.ConfigurationService = configurationService;
        }

        /// <inheritdoc />
        public ItemContentTaggingProvidersSet Build(string providersSetName)
        {
            ItemContentTaggingConfiguration configurationByName = this.ConfigurationService.GetConfigurationByName(providersSetName);
            return new RelatedItemContentTaggingProvidersSet
            {
                ContentProviders = configurationByName.ContentProviders.Select(cp => this.ProvidersFactory.CreateContentProvider<Item>(cp)).Where(new Func<IContentProvider<Item>, bool>(this.NotNull<IContentProvider<Item>>)),
                DiscoveryProviders = configurationByName.DiscoveryProviders.Select(cp => this.ProvidersFactory.CreateRelatedItemsDiscoveryProvider(cp)).Where(new Func<IRelatedItemsDiscoveryProvider, bool>(this.NotNull<IRelatedItemsDiscoveryProvider>)),
                TaxonomyProviders = configurationByName.TaxonomyProviders.Select(cp => this.ProvidersFactory.CreateTaxonomyProvider(cp)).Where(new Func<ITaxonomyProvider, bool>(this.NotNull<ITaxonomyProvider>)),
                Taggers = configurationByName.Taggers.Select(cp => this.ProvidersFactory.CreateTagger<Item>(cp)).Where(new Func<ITagger<Item>, bool>(this.NotNull<ITagger<Item>>))
            };
        }

        /// <summary>Checks whether object is not null</summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        protected virtual bool NotNull<T>(T arg)
        {
            return (object)arg != null;
        }
    }

    public interface IRelatedItemContentTaggingProviderSetBuilder
    {
        ItemContentTaggingProvidersSet Build(string providersSetName);
    }
}