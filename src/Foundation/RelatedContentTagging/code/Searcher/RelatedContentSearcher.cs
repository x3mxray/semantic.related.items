﻿using System;
using System.Collections.Generic;
using System.Linq;
using Semantic.Foundation.ML;
using Semantic.Foundation.RelatedContentTagging.Searcher.SearchResultItem;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.Linq.Utilities;
using Sitecore.Data;

namespace Semantic.Foundation.RelatedContentTagging.Searcher
{
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

        public IEnumerable<ContentObject> GetAll()
        {
            var index = ContentSearchManager.GetIndex(this.IndexName);
            using (var context = index.CreateSearchContext())
            {
                var query = context.GetQueryable<ItemWithVectorResultItem>();
                var results = query
                    .Select(x => new { x.ItemId, x.Vectors })
                    .GetResults()
                    .Select(x => x.Document)
                    .Where(x => x != null && x.Vectors != null)
                    .Select(x => new ContentObject
                    {
                        Id = x.ItemId.ToGuid(),
                        TextVector = x.Vectors.ToArray()
                    });

                return results;
            }
        }

        public IEnumerable<ContentObject> GetItemsByRelatedTemplates(IEnumerable<Guid> relatedTemplates)
        {
            var index = ContentSearchManager.GetIndex(this.IndexName);
            using (var context = index.CreateSearchContext())
            {
                var query = context.GetQueryable<ItemWithVectorResultItem>();
                var predicate = PredicateBuilder.True<ItemWithVectorResultItem>();

                foreach (var relatedTemplate in relatedTemplates)
                {
                    ID itemID = new ID(relatedTemplate);
                    predicate = predicate.Or(p => p.TemplateId == itemID);
                }

                var results = query.Filter(predicate)
                    // optimize to [fl=itemId,vector] only
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