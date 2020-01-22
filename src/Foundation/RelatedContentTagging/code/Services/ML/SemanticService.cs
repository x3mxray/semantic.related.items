using Hackathon.Boilerplate.Foundation.ML;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hackathon.Boilerplate.Foundation.RelatedContentTagging.Services.ML
{
    public class SemanticService : ISemanticService
    {
        public SemanticService()
        {
            Content2Vec.InitDataset(Sitecore.Configuration.Settings.GetSetting("SemanticDatasetFilePath"));
        }
        public float[] Vectorize(string content)
        {
            var vector = Content2Vec.Vectorization(content);
            return vector;
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
        float[] Vectorize(string content);
        IEnumerable<Guid> GetRelated(Guid itemId, IEnumerable<Guid> relatedTemplates);
    }
}