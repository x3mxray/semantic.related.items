using System.Collections.Generic;
using Hackathon.Boilerplate.Foundation.RelatedContentTagging.Models;
using Sitecore.ContentTagging.Core.Messaging;
using Sitecore.Pipelines;

namespace Hackathon.Boilerplate.Foundation.RelatedContentTagging.Pipelines.NormalizeContent
{
    public class NormalizeContentArgs : PipelineArgs
    {
        public MessageBus MessageBus { get; set; }

        public IEnumerable<RelatedTaggableContent> Content { get; set; }
    }
}