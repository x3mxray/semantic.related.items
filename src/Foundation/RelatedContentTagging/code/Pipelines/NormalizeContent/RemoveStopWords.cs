using System;
using System.Linq;
using Semantic.Foundation.RelatedContentTagging.Models;

namespace Semantic.Foundation.RelatedContentTagging.Pipelines.NormalizeContent
{
    public class RemoveStopWords
    {
        public void Process(NormalizeContentArgs args)
        {
            foreach (RelatedTaggableContent taggableContent in args.Content)
            {
                taggableContent.Content = DeleteStopWords(taggableContent.Content);
            }
        }

        protected virtual string DeleteStopWords(string text)
        {
            text = text.Trim().ToLower();
            var punctuation = text.Where(char.IsPunctuation).Distinct().ToArray().Append(' ').Append('\u0009').ToArray();

            var res = text.Split(punctuation, StringSplitOptions.RemoveEmptyEntries).ToArray();
            var cleanWords = res.Except(StopWords.stopWordsList).Where(x=> x.Length > 2).ToArray();

            return string.Join(" ",cleanWords);
        }
    }
}