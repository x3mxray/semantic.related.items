namespace Hackathon.Boilerplate.Foundation.RelatedContentTagging.Searcher.SearchResultItem
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.Serialization;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.Converters;
    using Sitecore.ContentSearch.SearchTypes;
    using Sitecore.Data;

    public class ItemWithVectorResultItem
    {
        [IndexField("_group")]
        [TypeConverter(typeof(IndexFieldIDValueConverter))]
        [DataMember]
        public virtual ID ItemId { get; set; }

        [IndexField("_template")]
        [TypeConverter(typeof(IndexFieldIDValueConverter))]
        [DataMember]
        public virtual ID TemplateId { get; set; }

        [IndexField("vector")]
        public virtual IEnumerable<float> Vectors { get; set; }
    }
}