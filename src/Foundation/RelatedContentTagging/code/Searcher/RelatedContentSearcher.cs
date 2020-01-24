using Hackathon.Boilerplate.Foundation.ML;

namespace Hackathon.Boilerplate.Foundation.RelatedContentTagging.Searcher
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SearchResultItem;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.Linq;
    using Sitecore.ContentSearch.Linq.Utilities;
    using Sitecore.Data;

    public class RelatedContentSearcher : IRelatedContentSearcher
    {
        protected string IndexName => Sitecore.Configuration.Settings.GetSetting("RelatedContentIndexName");

        public ContentObject GetCurrentItemFromSolr(Guid itemId)
        {
            var index = ContentSearchManager.GetIndex(this.IndexName);
            using (var context = index.CreateSearchContext())
            {
                ID itemID = new ID(itemId);
                var query = context.GetQueryable<ItemWithVectorResultItem>();
                query = query.Where(i =>
                    i.ItemId == itemID);
                var result = query
                    .Select(x => new {x.ItemId, x.Vectors})
                    .GetResults()?.FirstOrDefault()?.Document;
                    
                return result==null ? null : new ContentObject{Id = result.ItemId.Guid, TextVector = result.Vectors?.ToArray()} ;
            }
        }

        public IEnumerable<ContentObject> GetItemsByRelatedTemplates(Guid itemId, IEnumerable<Guid> relatedTemplates)
        {
            var index = ContentSearchManager.GetIndex(this.IndexName);
            using (var context = index.CreateSearchContext())
            {
                ID currentItemId = new ID(itemId);
                var query = context.GetQueryable<ItemWithVectorResultItem>();
                var predicate = PredicateBuilder.True<ItemWithVectorResultItem>();

                foreach (var relatedTemplate in relatedTemplates)
                {
                    ID itemID = new ID(relatedTemplate);
                    predicate = predicate.Or(p => p.TemplateId == itemID);
                }

                var results = query.Where(x => x.ItemId != currentItemId).Filter(predicate)
                    .Select(x => new{ x.ItemId, x.Vectors})
                    .GetResults()
                    .Select(x => x.Document)
                    .Where(x => x!=null && x.Vectors!=null)
                    .Select(x => new ContentObject
                    {
                        Id = x.ItemId.ToGuid(),
                        TextVector = x.Vectors.ToArray()
                    });
               
                return results;
            }
        }
    }
}