using System.Collections.Generic;
using Sitecore.ContentTagging.Core.Providers;
using Sitecore.Data.Items;

namespace Hackathon.Boilerplate.Foundation.RelatedContentTagging.Providers
{
    public class RelatedItemContentTaggingProvidersSet 
    {
        public IEnumerable<IContentProvider<Item>> ContentProviders { get; set; }

        public IRelatedItemsDiscoveryProvider DiscoveryProvider { get; set; }

        public IEnumerable<IRelatedItemsTagger<Item>> Taggers { get; set; }

    }
}