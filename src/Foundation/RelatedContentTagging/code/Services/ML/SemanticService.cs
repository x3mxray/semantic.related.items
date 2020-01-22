using Hackathon.Boilerplate.Foundation.ML;
using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Buckets.Extensions;

namespace Hackathon.Boilerplate.Foundation.RelatedContentTagging.Services.ML
{
    using Searcher;

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

        public IEnumerable<Guid> GetRelated(Guid itemId, IEnumerable<Guid> relatedTemplates)
        {
            var current = this.contentSearcher.GetCurrentItemFromSolr(itemId);
            var list = this.contentSearcher.GetItemsByRelatedTemplates(relatedTemplates);
            var related = Content2Vec.NearestItems(current.TextVector, list.Where(x => x.Id!=itemId).ToList());

            return related;
        }
    }

    public interface ISemanticService
    {
        float[] Vectorize(string content);
        IEnumerable<Guid> GetRelated(Guid itemId, IEnumerable<Guid> relatedTemplates);
    }
}