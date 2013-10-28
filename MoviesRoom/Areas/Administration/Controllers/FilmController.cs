using Kendo.Mvc.UI;
using MoviesRoom.Controllers;
using MoviesRoom.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using MoviesRoom.ViewModels.Film;
using System.Data.Entity;
using MoviesRoom.Areas.Administration.ViewModels;
using MoviesRoom.Models;

namespace MoviesRoom.Areas.Administration.Controllers
{

    [Authorize(Roles = "admin")]
    public class FilmController : BaseController
    {
        public FilmController()
            : base(new UowData())
        {
        }

        public FilmController(IUowData data)
            : base(data)
        {
        }

        public JsonResult GetAllFilms([DataSourceRequest] DataSourceRequest request)
        {
            var result = this.Data.Films.All().Select(GridFilmViewModel.FromFilm);
            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateFilm([DataSourceRequest] DataSourceRequest request, GridFilmViewModel film)
        {
            if (film != null && ModelState.IsValid)
            {
                var filmEntity = this.Data.Films.GetById(film.Id);
                var category = this.Data.Categories.GetById(film.CategoryId);

                filmEntity.AvailableTickets = film.AvailableTickets;
                filmEntity.Category = category;
                filmEntity.Description = film.Description;
                filmEntity.PricePerTicket = film.PricePerTicket;
                filmEntity.StartDate = film.StartDate;
                filmEntity.Title = film.Title;

                this.Data.Films.Update(filmEntity);
                this.Data.SaveChanges();

                film.Id = filmEntity.Id;
                film.CategoryName = category.Name;
            }

            return Json(new[] { film }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreateFilm([DataSourceRequest] DataSourceRequest request, GridFilmViewModel film)
        {
            if (film != null && ModelState.IsValid)
            {
                var category = this.Data.Categories.GetById(film.CategoryId);

                var filmEntity = new Film()
                {
                    AvailableTickets = film.AvailableTickets,
                    CategoryId = film.CategoryId,
                    Description = film.Description,
                    PricePerTicket = film.PricePerTicket,
                    StartDate = film.StartDate,
                    Title = film.Title,
                };

                this.Data.Films.Add(filmEntity);
                this.Data.SaveChanges();

                film.Id = filmEntity.Id;
                film.CategoryName = category.Name;
            }

            return Json(new[] { film }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteFilm([DataSourceRequest] DataSourceRequest request, GridFilmViewModel film)
        {
            if (film != null && ModelState.IsValid)
            {
                this.Data.Films.Delete(film.Id);
                this.Data.SaveChanges();
            }

            return Json(new[] { film }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }
    }
}