using System;
using System.Collections.Generic;
using Semantic.Foundation.ML;

namespace Semantic.Foundation.RelatedContentTagging.Searcher
{
    public interface IRelatedContentSearcher
    {
        ContentObject GetCurrentItemFromSolr(Guid itemId);

        IEnumerable<ContentObject> GetItemsByRelatedTemplates(IEnumerable<Guid> relatedTemplates);
        IEnumerable<ContentObject> GetAll();
    }
}