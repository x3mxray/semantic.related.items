using System;
using Sitecore.ContentTagging.Core.Models;

namespace Hackathon.Boilerplate.Foundation.RelatedContentTagging.Models
{
    public class RelatedTaggableContent : TaggableContent
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
    }
}