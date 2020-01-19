using System;
using Sitecore.ContentTagging.Core.Messaging;
using Sitecore.ContentTagging.Core.Models;
using Sitecore.Data.Items;
using Sitecore.Pipelines;
using System.Collections.Generic;
using Hackathon.Boilerplate.Foundation.RelatedContentTagging.Providers;

namespace Hackathon.Boilerplate.Foundation.RelatedContentTagging.Pipelines.TagContent
{
    public class RelatedContentTagArgs : PipelineArgs
    {
        public Item ContentItem { get; set; }

        /// <summary>Message bust to push messages from Message Sources</summary>
        public MessageBus MessageBus { get; set; }

        /// <summary>Set of providers for running Item Content Tagging</summary>
        public RelatedItemContentTaggingProvidersSet Configuration { get; set; }

        /// <summary>Taggable content</summary>
        public IEnumerable<TaggableContent> Content { get; set; }

        /// <summary>Related items collection</summary>
        public IEnumerable<Guid> Tags { get; set; }
    }
}