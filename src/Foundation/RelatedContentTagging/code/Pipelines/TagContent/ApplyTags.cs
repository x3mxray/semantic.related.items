using Hackathon.Boilerplate.Foundation.RelatedContentTagging.Providers;
using Sitecore.Data.Items;

namespace Hackathon.Boilerplate.Foundation.RelatedContentTagging.Pipelines.TagContent
{
    public class ApplyTags
    {
        public void Process(RelatedContentTagArgs args)
        {
            foreach (IRelatedItemsTagger<Item> tagger in args.Configuration.Taggers)
            {
                tagger.TagContent(args.ContentItem, args.RelatedItems);  
            }

        }
    }
}