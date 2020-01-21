using System.Collections.Generic;
using Sitecore.ContentTagging.Core.Providers;
using Sitecore.Data.Items;

namespace Hackathon.Boilerplate.Foundation.RelatedContentTagging.Providers
{
    public class RelatedItemContentTaggingProvidersSet 
    {
        public IEnumerable<IContentProvider<Item>> ContentProviders { get; set; }

        public IEnumerable<IRelatedItemsDiscoveryProvider> DiscoveryProviders { get; set; }

        public IEnumerable<ITagger<Item>> Taggers { get; set; }

    }
}