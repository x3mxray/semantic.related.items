using System;
using System.Collections.Generic;
using Semantic.Foundation.RelatedContentTagging.Models;
using Semantic.Foundation.RelatedContentTagging.Providers;
using Sitecore.ContentTagging.Core.Messaging;
using Sitecore.Data.Items;
using Sitecore.Pipelines;

namespace Semantic.Foundation.RelatedContentTagging.Pipelines.TagContent
{
    public class RelatedContentTagArgs : PipelineArgs
    {
        public Item ContentItem { get; set; }

        /// <summary>Message bust to push messages from Message Sources</summary>
        public MessageBus MessageBus { get; set; }

        /// <summary>Set of providers for running Item Content Tagging</summary>
        public RelatedItemContentTaggingProvidersSet Configuration { get; set; }

        /// <summary>Taggable content</summary>
        public IEnumerable<RelatedTaggableContent> Content { get; set; }

        /// <summary>Content vector</summary>
        public float[] Vector { get; set; }

        /// <summary>Related items collection</summary>
        public IEnumerable<Guid> RelatedItems { get; set; }
    }
}