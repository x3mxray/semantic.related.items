using System;
using System.Collections.Generic;
using System.Linq;

namespace Hackathon.Boilerplate.Foundation.RelatedContentTagging.Services.ML
{
    public class SemanticService : ISemanticService
    {
        public double[] Vectorize(string content)
        {
            return new double[] {0.111, 0.222, 0.333};
        }

        public IEnumerable<Guid> GetRelated(Guid itemId, IEnumerable<Guid> relatedTemplates)
        {
            var current = GetCurrentItemFromSolr(itemId);
            var list = GetAllFromSolr(relatedTemplates);

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