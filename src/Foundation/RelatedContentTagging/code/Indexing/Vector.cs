using System.Linq;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Data.Items;

namespace Hackathon.Boilerplate.Foundation.RelatedContentTagging.Indexing
{
    public class Vector : IComputedIndexField
    {
        public object ComputeFieldValue(IIndexable indexable)
        {
            var item = (Item)(indexable as SitecoreIndexableItem);

            if (item == null)
            {
                return null;
            }

            var fakeData = new double[] {0.111, 0.222, 0.333};

            return fakeData.ToList();
        }

        public string FieldName { get; set; }
        public string ReturnType { get; set; }
    }
}