using System;
using System.Collections.Generic;
using System.Linq;
using Semantic.Foundation.ML;
using Semantic.Foundation.RelatedContentTagging.Searcher;

namespace Semantic.Foundation.RelatedContentTagging.Services.ML
{
    public class SemanticService : ISemanticService
    {
        protected IRelatedContentSearcher contentSearcher;

        public SemanticService(IRelatedContentSearcher contentSearcher)
        {
            this.contentSearcher = contentSearcher;
            Content2Vec.InitDataset(Sitecore.Configuration.Settings.GetSetting("SemanticDatasetFilePath"));
        }

        public float[] Vectorize(string content)
        {
            var vector = Content2Vec.Vectorization(content);
            return vector;
        }

        public IEnumerable<Guid> GetRelated(Guid itemId, IEnumerable<Guid> relatedTemplates, int similarity)
        {
            if (relatedTemplates == null || !relatedTemplates.Any())
                return null;

            var current = contentSearcher.GetCurrentItemFromSolr(itemId);
            if (current?.TextVector == null)
                return null;

            var list = contentSearcher.GetItemsByRelatedTemplates(relatedTemplates);

            if (list == null || !list.Any())
                return null;

            var related = Content2Vec.NearestItems(current.TextVector, list.Where(x => x.Id!=itemId).ToList(), similarity);

            return related;
        }

        public IEnumerable<Guid> Search(string text, int? similarity)
        {
            var vector = Vectorize(text.ToLower());
            var texts = contentSearcher.GetAll();
            var results = Content2Vec.NearestItems(vector, texts.ToList(), similarity ?? 0);

            return results;
        }
    }

    public interface ISemanticService
    {
        float[] Vectorize(string content);
        IEnumerable<Guid> GetRelated(Guid itemId, IEnumerable<Guid> relatedTemplates, int similarity);
        IEnumerable<Guid>  Search(string text, int? similarity);
    }
}