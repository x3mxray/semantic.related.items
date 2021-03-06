﻿using Microsoft.Extensions.DependencyInjection;
using Semantic.Foundation.RelatedContentTagging.Pipelines.TagContent;
using Semantic.Foundation.RelatedContentTagging.Providers;
using Sitecore.Abstractions;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.ContentTagging.Core.Messaging;
using Sitecore.Data.Items;
using Sitecore.DependencyInjection;

namespace Semantic.Foundation.RelatedContentTagging.Indexing
{
    public class Vector : IComputedIndexField
    {
        public object ComputeFieldValue(IIndexable indexable)
        {
            var item = (Item)(indexable as SitecoreIndexableItem);

            if (item == null)
            {
                return null;
            }

            var vector = GetItemVector(item);

            return vector;
        }

        public float[] GetItemVector(Item item)
        {
            var messageBusFactory = ServiceLocator.ServiceProvider.GetService<IMessageBusFactory>();
            var messageBus = messageBusFactory.Create();
            BaseCorePipelineManager PipelineManager = ServiceLocator.ServiceProvider.GetService<BaseCorePipelineManager>(); ;
            string pipelineDomain = "RealtedContentTagging";

            var configurationArgs = new GetRelatedContentTaggingConfigurationArgs
            {
                MessageBus = messageBus
            };
            PipelineManager.Run("getRelatedTaggingConfiguration", configurationArgs, pipelineDomain);
            BaseCorePipelineManager pipelineManager = PipelineManager;
            var tagContentArgs = new RelatedContentTagArgs
            {
                Configuration = new RelatedItemContentTaggingProvidersSet
                {
                    ContentProviders = configurationArgs.ProvidersSet.ContentProviders,
                    Taggers = configurationArgs.ProvidersSet.Taggers,
                    DiscoveryProvider = configurationArgs.ProvidersSet.DiscoveryProvider,

                },
                ContentItem = item,
                MessageBus = messageBus
            };

            pipelineManager.Run("getContent", tagContentArgs, pipelineDomain);
            return tagContentArgs.Vector;
        }

        public string FieldName { get; set; }
        public string ReturnType { get; set; }
    }
}