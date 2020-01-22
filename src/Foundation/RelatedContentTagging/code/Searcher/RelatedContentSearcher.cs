namespace Hackathon.Boilerplate.Foundation.RelatedContentTagging.Searcher
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SearchResultItem;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.Linq;
    using Sitecore.ContentSearch.Linq.Utilities;
    using Sitecore.ContentSearch.SearchTypes;
    using Sitecore.Data;

    public class RelatedContentSearcher : IRelatedContentSearcher
    {
        protected string IndexName => Sitecore.Configuration.Settings.GetSetting("RelatedContentIndexName");

        public Sitecore.ContentSearch.SearchTypes.SearchResultItem GetCurrentItemFromSolr(Guid itemId)
        {
            var index = ContentSearchManager.GetIndex(this.IndexName);
            using (var context = index.CreateSearchContext())
            {
                ID itemID = new ID(itemId);
                var query = context.GetQueryable<Sitecore.ContentSearch.SearchTypes.SearchResultItem>();
                query = query.Where(i =>
                    i.ItemId == itemID);
                var result = query.GetResults()?.FirstOrDefault()?.Document;
                return result;
            }
        }

        public IEnumerable<ItemWIthVectorResultItem> GetItemsByRelatedTemplates(IEnumerable<Guid> relatedTemplates)
        {
            var index = ContentSearchManager.GetIndex(this.IndexName);
            using (var context = index.CreateSearchContext())
            {
                //TODO change where, need search by site field instead Paths
                var query = context.GetQueryable<ItemWIthVectorResultItem>();
                var predicate = PredicateBuilder.True<ItemWIthVectorResultItem>();
                foreach (var relatedTemplate in relatedTemplates)
                {
                    ID itemID = new ID(relatedTemplate);
                    predicate = predicate.Or(p => p.TemplateId == itemID);
                }
                query = query.Filter(predicate);
                var result = query.GetResults()?.Select(x => x.Document);
                return result;
            }
        }
    }
}