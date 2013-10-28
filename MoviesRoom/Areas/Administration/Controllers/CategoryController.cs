using Kendo.Mvc.UI;
using MoviesRoom.Areas.Administration.ViewModels;
using MoviesRoom.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using MoviesRoom.Data;

namespace MoviesRoom.Areas.Administration.Controllers
{
    [Authorize(Roles="admin")]
    public class CategoryController : BaseController
    {
        public CategoryController() : base(new UowData())
        {
        }

        public CategoryController(IUowData data) : base(data)
        {
        }
       
        public JsonResult GetAllCategories([DataSourceRequest]
                                           DataSourceRequest request)
        {
            var result = this.Data.Categories.All()
                             .Select(CategoryViewModel.FromCategory);

            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateCategory([DataSourceRequest]
                                         DataSourceRequest request, CategoryViewModel category)
        {
            if (category != null && ModelState.IsValid)
            {
                this.Data.Categories.Update(category.CreateCategory());
                this.Data.SaveChanges();
            }

            return Json(new[] { category }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreateCategory([DataSourceRequest]
                                         DataSourceRequest request,
            CategoryViewModel category)
        {
            if (category != null && ModelState.IsValid)
            {
                this.Data.Categories.Add(category.CreateCategory());
                this.Data.SaveChanges();
            }

            return Json(new[] { category }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteCategory([DataSourceRequest]
                                         DataSourceRequest request,
            CategoryViewModel category)
        {
            if (category != null && ModelState.IsValid)
            {
                this.Data.Categories.Delete(category.CreateCategory());
                this.Data.SaveChanges();
            }

            return Json(new[] { category }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }
    }
}