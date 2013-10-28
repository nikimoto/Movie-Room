using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using MoviesRoom.Areas.Administration.ViewModels;
using MoviesRoom.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MoviesRoom.Data;

namespace MoviesRoom.Areas.Administration.Controllers
{
    public class AdminTicketController : BaseController
    {
        public AdminTicketController()
            : base(new UowData())
        {
        }

        public AdminTicketController(IUowData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ReadTickets([DataSourceRequest] DataSourceRequest request)
        {
            var tickets = this.Data.Tickets.All().Select(AdminTicketViewModel.FromTicket);
            return Json(tickets.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteTicket([DataSourceRequest] DataSourceRequest request, AdminTicketViewModel ticket)
        {
            if (ticket != null && ModelState.IsValid)
            {
                var filmEntity = this.Data.Films.GetById(ticket.FilmId);
                filmEntity.AvailableTickets++;
                this.Data.Films.Update(filmEntity);

                this.Data.Tickets.Delete(ticket.Id);
                this.Data.SaveChanges();
            }

            return Json(new[] { ticket }.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}