using Hackathon.Boilerplate.Foundation.RelatedContentTagging.Models;
using Hackathon.Boilerplate.Foundation.RelatedContentTagging.Services.ML;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.ContentTagging.Core.Messaging;
using Sitecore.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Hackathon.Boilerplate.Foundation.RelatedContentTagging.Providers
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

        public IEnumerable<Guid> GetRelatedItems(Guid itemId, IEnumerable<Guid> relatedTemplates)
        {
            return _semanticService.GetRelated(itemId, relatedTemplates);
        }

        public float[] GetVector(RelatedTaggableContent content)
        {
            return _semanticService.Vectorize(content.Content);
        }
    }

    public interface IRelatedItemsDiscoveryProvider
    {
        IEnumerable<Guid> GetRelatedItems(Guid itemId, IEnumerable<Guid> relatedTemplates);
        float[] GetVector(RelatedTaggableContent content);
    }
}