using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Semantic.Foundation.RelatedContentTagging;
using Semantic.Foundation.RelatedContentTagging.Services.ML;
using Semantic.Project.Demo.Models;
using Sitecore.Data;
using Sitecore.Links;

namespace Semantic.Project.Demo.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISemanticService _semanticService;

        public SearchController(ISemanticService semanticService)
        {
            _semanticService = semanticService;
        }

        public ActionResult Index()
        {
           return View();
        }

        public ActionResult Overview()
        {
            var model = new OverviwModel
            {
                Products = Convert(Sitecore.Context.Database.GetItem(new ID("{29B7F8CE-DFC5-4996-BF6E-17703406A293}"))
                    .Axes.GetDescendants().Select(x => x.ID.Guid)),

                Articles = Convert(Sitecore.Context.Database.GetItem(new ID("{2CF474A3-6681-4A0E-8C29-D8EAA386BD8B}"))
                    .Axes.GetDescendants().Select(x => x.ID.Guid)),

                News = Convert(Sitecore.Context.Database.GetItem(new ID("{03CD14C6-11EE-4AB8-BE33-B6B627439633}"))
                    .Axes.GetDescendants().Select(x => x.ID.Guid)),
            };

            return View(model);
        }

        public ActionResult Search(string term, int? similarity)
        {
            if (string.IsNullOrEmpty(term))
                return View();

            var results = _semanticService.Search(term, similarity ?? 0);
            var model = Convert(results);

            return View(model);
        }

        public ActionResult Related()
        {
            var item = Sitecore.Context.Item;

            Sitecore.Data.Fields.MultilistField relatedTemplates = item.Fields[Constants.Fields.RetatedTemplates];
            var relatedTemplatesIds = relatedTemplates.TargetIDs.Select(x => x.Guid);

            int.TryParse(item.Fields[Constants.Fields.Similarity].Value, out var similarity);

            var relatedItems = _semanticService.GetRelated(item.ID.Guid, relatedTemplatesIds, similarity);

            if (relatedItems == null || !relatedItems.Any())
                return View();

            var model = Convert(relatedItems);

            return View(model);
        }

        List<SearchViewResult> Convert(IEnumerable<Guid> ids)
        {
            var results = new List<SearchViewResult>();
            foreach (var entity in ids)
            {
                var item = Sitecore.Context.Database.GetItem(new ID(entity));
                if (item != null)
                {
                    results.Add(new SearchViewResult
                    {
                        Title = item.Fields["Title"]!=null ? item.Fields["Title"].Value : item.Name,
                        Desciption = item.Fields["Description"] != null 
                            ? item.Fields["Description"].Value
                            : item.Fields["Information"] != null 
                                ? item.Fields["Information"].Value
                                : item.Fields["Body"].Value,
                        Url = LinkManager.GetItemUrl(item)
                    });
                }
               
            }
            return results;
        }
    }
}