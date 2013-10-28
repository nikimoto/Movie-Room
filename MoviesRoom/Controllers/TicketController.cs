using MoviesRoom.Data;
using MoviesRoom.Models;
using MoviesRoom.ViewModels.Film;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace MoviesRoom.Controllers
{
    [Authorize]
    public class TicketController : BaseController
    {
        public TicketController()
            : base(new UowData())
        {
        }

        public TicketController(IUowData data)
            : base(data)
        {
        }

        public ActionResult GenerateTickets(int filmId, int ticketsCount = 0)
        {
            var film = this.Data.Films.GetById(filmId);

            if (film == null)
            {
                return GoBack(filmId);
            }

            if (film.AvailableTickets < ticketsCount)
            {
                return GoBack(filmId);
            }

            if (ticketsCount <= 0)
            {
                return GoBack(filmId);
            }

            Guid referenceCode = Guid.NewGuid();
            var user = this.Data.Users.All()
                .FirstOrDefault(u => u.UserName == this.User.Identity.Name);

            ICollection<Ticket> tickets = new List<Ticket>();

            for (int i = 0; i < ticketsCount; i++)
            {
                var newTicket = new Ticket()
                {
                    DateOfPurchase = DateTime.Now,
                    Film = film,
                    Price = film.PricePerTicket,
                    User = user,
                    ReferenceCode = referenceCode,
                };

                tickets.Add(newTicket);
                this.Data.Tickets.Add(newTicket);
            }

            try
            {
                film.AvailableTickets -= ticketsCount;
                this.Data.SaveChanges();
            }
            catch (Exception ex)
            {
                return GoBack(filmId);
            }

            return View("ViewTickets", tickets);
        }

        private ActionResult GoBack(int filmId)
        {
            return RedirectToAction("SingleFilm", "Home", new { id = filmId });
        }

        public ActionResult MyTickets()
        {
            var tickets = this.Data.Users.All().Include(u => u.Tickets)
                .FirstOrDefault(u => u.UserName == this.User.Identity.Name)
                .Tickets.OrderByDescending(t=> t.DateOfPurchase);

            return View("ViewTickets", tickets);
        }
    }
}