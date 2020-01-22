namespace Hackathon.Boilerplate.Foundation.RelatedContentTagging.Searcher
{
    using System;
    using System.Collections.Generic;
    using SearchResultItem;
    using Sitecore.ContentSearch.SearchTypes;

    public interface IRelatedContentSearcher
    {
        Sitecore.ContentSearch.SearchTypes.SearchResultItem GetCurrentItemFromSolr(Guid itemId);

        IEnumerable<ItemWIthVectorResultItem> GetItemsByRelatedTemplates(IEnumerable<Guid> relatedTemplates);
    }
}