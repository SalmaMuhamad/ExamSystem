using QuizbeePlus.Services;
using QuizbeePlus.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizbeePlus.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        
        public ActionResult Index(string search, int? page, int? items)
        {
            HomeViewModel model = new HomeViewModel();
            model.PageInfo = new PageInfo()
            {
                PageTitle = "Home Page",
                PageDescription = "Description"
            };
            model.searchTerm = search;
            model.pageNo = page ?? 1;
            model.pageSize = items ?? 9;

            var categoriesSearch = CategoryService.Instance.GetCategories(model.searchTerm, model.pageNo, model.pageSize);

            model.Categories = categoriesSearch.Categories;
            model.TotalCount = categoriesSearch.TotalCount;

            model.Pager = new Pager(model.TotalCount, model.pageNo, model.pageSize);
            return View(model);
        }

        public ActionResult Initial()
        {
            return View();
        }
    }
}