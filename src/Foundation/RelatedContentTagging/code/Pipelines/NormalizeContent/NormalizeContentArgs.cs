using System.Collections.Generic;
using Semantic.Foundation.RelatedContentTagging.Models;
using Sitecore.ContentTagging.Core.Messaging;
using Sitecore.Pipelines;

namespace Semantic.Foundation.RelatedContentTagging.Pipelines.NormalizeContent
{
    public class NormalizeContentArgs : PipelineArgs
    {
        public MessageBus MessageBus { get; set; }

        public IEnumerable<RelatedTaggableContent> Content { get; set; }
    }
}