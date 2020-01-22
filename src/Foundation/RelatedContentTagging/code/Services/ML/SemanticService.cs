using Hackathon.Boilerplate.Foundation.ML;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hackathon.Boilerplate.Foundation.RelatedContentTagging.Services.ML
{
    using Searcher;

    public class SemanticService : ISemanticService
    {
        protected IRelatedContentSearcher contentSearcher;

        public SemanticService(IRelatedContentSearcher contentSearcher)
        {
            this.contentSearcher = contentSearcher;
        }

        public double[] Vectorize(string content)
        {
            var vector = Content2Vec.Vectorization(content);
            return vector;
        }

        public IEnumerable<Guid> GetRelated(Guid itemId, IEnumerable<Guid> relatedTemplates)
        {
            var current = this.contentSearcher.GetCurrentItemFromSolr(itemId);
            var list = this.contentSearcher.GetItemsByRelatedTemplates(relatedTemplates);

            var related = Nearest(current, list);

            return related;
        }

        IEnumerable<Guid> Nearest(object currentItem, IEnumerable<object> list)
        {
            // TODO: implement
            return Enumerable.Empty<Guid>();
        }


        object GetCurrentItemFromSolr(Guid item)
        {
            // TODO: request to Sorl
            return new object();
        }

        IEnumerable<object> GetAllFromSolr(IEnumerable<Guid> relatedTemplates)
        {
            // TODO: request to Sorl
            return Enumerable.Empty<object>();
        }
    }

    public interface ISemanticService
    {
        double[] Vectorize(string content);
        IEnumerable<Guid> GetRelated(Guid itemId, IEnumerable<Guid> relatedTemplates);
    }
}