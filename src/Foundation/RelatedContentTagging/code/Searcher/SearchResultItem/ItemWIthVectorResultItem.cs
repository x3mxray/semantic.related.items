namespace Hackathon.Boilerplate.Foundation.RelatedContentTagging.Searcher.SearchResultItem
{
    using System.Collections.Generic;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.SearchTypes;

    public class ItemWithVectorResultItem : SearchResultItem
    {
        [IndexField("vector")]
        public virtual IEnumerable<float> Vectors { get; set; }
    }
}