using System.Collections.Generic;

namespace Semantic.Project.Demo.Models
{
    public class OverviwModel
    {
        public List<SearchViewResult> Products { get; set; }
        public List<SearchViewResult> News { get; set; }
        public List<SearchViewResult> Articles { get; set; }
    }

    public class SearchViewResult
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string Desciption { get; set; }
    }
}