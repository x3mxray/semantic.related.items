using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.ContentTagging.Configuration;
using Sitecore.ContentTagging.Core.Providers;
using Sitecore.Data.Items;

namespace Hackathon.Boilerplate.Foundation.RelatedContentTagging.Providers
{
    public class RelatedItemContentTaggingProvidersSet : ItemContentTaggingProvidersSet
    {
     
      //  public IEnumerable<IContentProvider<Item>> ContentProviders { get; set; }
       
        public new IEnumerable<IRelatedItemsDiscoveryProvider> DiscoveryProviders { get; set; }
     
      //  public IEnumerable<ITaxonomyProvider> TaxonomyProviders { get; set; }
      
     //   public IEnumerable<ITagger<Item>> Taggers { get; set; }
    }
}