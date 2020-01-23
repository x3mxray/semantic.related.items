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
            ProvidersFactory = providersFactory;
            ConfigurationService = configurationService;
        }

        public RelatedItemContentTaggingProvidersSet Build(string providersSetName)
        {
            ItemContentTaggingConfiguration configurationByName = this.ConfigurationService.GetConfigurationByName(providersSetName);
            var test= new RelatedItemContentTaggingProvidersSet
            {
                ContentProviders = configurationByName.ContentProviders.Select(cp => this.ProvidersFactory.CreateContentProvider<Item>(cp)).Where(new Func<IContentProvider<Item>, bool>(this.NotNull<IContentProvider<Item>>)),
                DiscoveryProvider = configurationByName.DiscoveryProviders.Select(cp => this.ProvidersFactory.CreateDiscoveryProvider(cp)).Where(new Func<IRelatedItemsDiscoveryProvider, bool>(this.NotNull<IRelatedItemsDiscoveryProvider>)).FirstOrDefault(),
                Taggers = configurationByName.Taggers.Select(cp => this.ProvidersFactory.CreateTagger<Item>(cp)).Where(new Func<IRelatedItemsTagger<Item>, bool>(this.NotNull<IRelatedItemsTagger<Item>>))
            };
            return test;
        }

        protected virtual bool NotNull<T>(T arg)
        {
            return (object)arg != null;
        }
    }

    public interface IRelatedItemContentTaggingProviderSetBuilder
    {
        RelatedItemContentTaggingProvidersSet Build(string providersSetName);
    }
}