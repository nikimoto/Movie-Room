using System;
using System.Linq;
using System.Web.Mvc;
using MoviesRoom.Data;
using MoviesRoom.Controllers;
using MoviesRoom.Areas.Administration.ViewModels;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace MoviesRoom.Areas.Administration.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : BaseController
    {
        public AdminController()
            : base(new UowData())
        {
        }

        public AdminController(IUowData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            this.ViewData.Add("categories", this.Data.Categories.All());

            return View();
        }

    }
}