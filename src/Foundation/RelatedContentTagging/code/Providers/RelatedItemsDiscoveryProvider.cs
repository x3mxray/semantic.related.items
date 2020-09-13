using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Semantic.Foundation.RelatedContentTagging.Services.ML;
using Sitecore.ContentTagging.Core.Messaging;
using Sitecore.DependencyInjection;

namespace Semantic.Foundation.RelatedContentTagging.Providers
{
    public class RelatedItemsDiscoveryProvider : MessageSource, IRelatedItemsDiscoveryProvider
    {
        private readonly ISemanticService _semanticService;
        public RelatedItemsDiscoveryProvider()
            : this(ServiceLocator.ServiceProvider.GetService<ISemanticService>())
        {
        }

        public RelatedItemsDiscoveryProvider(ISemanticService semanticService)
        {
            _semanticService = semanticService;
        }

        public IEnumerable<Guid> GetRelatedItems(Guid itemId, IEnumerable<Guid> relatedTemplates, int similarity)
        {
            return _semanticService.GetRelated(itemId, relatedTemplates, similarity);
        }

        public float[] GetVector(string content)
        {
            return _semanticService.Vectorize(content);
        }
    }

    public interface IRelatedItemsDiscoveryProvider
    {
        IEnumerable<Guid> GetRelatedItems(Guid itemId, IEnumerable<Guid> relatedTemplates, int similarity);
        float[] GetVector(string content);
    }
}