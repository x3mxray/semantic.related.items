using System;
using System.Collections.Generic;
using Sitecore.ContentTagging.Core.Messaging;
using Sitecore.ContentTagging.Core.Models;
using Sitecore.ContentTagging.Core.Providers;

namespace Hackathon.Boilerplate.Foundation.RelatedContentTagging.Providers
{
    public class NanDiscoveryProvider : MessageSource, IRelatedItemsDiscoveryProvider
    {
        public IEnumerable<Guid> GetRelatedItems(IEnumerable<TaggableContent> content)
        {
            var relatedItemsList = new List<Guid>();
            foreach (var  taggableContent in content)
            {
                var stringContent = (StringContent) taggableContent;
            }

            return relatedItemsList;
        }

        public IEnumerable<TagData> GetTags(IEnumerable<TaggableContent> content)
        {
            throw new NotImplementedException();
        }

        public bool IsConfigured()
        {
            return true;
        }
    }

    public interface IRelatedItemsDiscoveryProvider : IDiscoveryProvider
    {
        IEnumerable<Guid> GetRelatedItems(IEnumerable<TaggableContent> content);
    }
}