using Hackathon.Boilerplate.Foundation.ML;

namespace Hackathon.Boilerplate.Foundation.RelatedContentTagging.Searcher
{
    using System;
    using System.Collections.Generic;

    public interface IRelatedContentSearcher
    {
        ContentObject GetCurrentItemFromSolr(Guid itemId);

        IEnumerable<ContentObject> GetItemsByRelatedTemplates(Guid itemId, IEnumerable<Guid> relatedTemplates);
    }
}