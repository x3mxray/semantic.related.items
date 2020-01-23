using System;
using System.Collections.Generic;
using Sitecore.ContentTagging.Core.Messaging;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using System.Linq;

namespace Hackathon.Boilerplate.Foundation.RelatedContentTagging.Providers
{
    public class RelatedItemsTagger : MessageSource, IRelatedItemsTagger<Item>
    {
        public void TagContent(Item contentItem, IEnumerable<Guid> related)
        {
            MultilistField field = contentItem.Fields[Constants.Fields.RetatedItems];
            contentItem.Editing.BeginEdit();

            field.Value = "";
            foreach (Guid tag in related)
            {
                if (ID.TryParse(tag, out var result) && !field.TargetIDs.Contains(result))
                    field.Add(result.ToString());
            }
            contentItem.Editing.EndEdit();
        }
    }

    public interface IRelatedItemsTagger<in T>
    {
        void TagContent(T taggable, IEnumerable<Guid> related);
    }
}