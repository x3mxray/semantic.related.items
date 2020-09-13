using System.Collections.Generic;
using Sitecore.ContentSearch;

namespace Semantic.Foundation.RelatedContentTagging.Searcher.SearchResultItem
{
    public class ItemWithVectorResultItem : Sitecore.ContentSearch.SearchTypes.SearchResultItem
    {
        [IndexField("vector")]
        public virtual IEnumerable<float> Vectors { get; set; }
    }
}