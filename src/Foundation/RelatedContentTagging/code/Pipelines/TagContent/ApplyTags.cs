using Sitecore.ContentTagging.Core.Providers;
using Sitecore.Data.Items;

namespace Hackathon.Boilerplate.Foundation.RelatedContentTagging.Pipelines.TagContent
{
    public class ApplyTags
    {
        /// <summary>Perform action</summary>
        /// <param name="args"></param>
        public void Process(RelatedContentTagArgs args)
        {
            foreach (ITagger<Item> tagger in args.Configuration.Taggers)
            {
                //  tagger.TagContent(args.ContentItem, args.Tags);  
            }

        }
    }
}