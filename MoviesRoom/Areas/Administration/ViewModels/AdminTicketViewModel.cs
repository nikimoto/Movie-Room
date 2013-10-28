using MoviesRoom.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MoviesRoom.Areas.Administration.ViewModels
{
    public class AdminTicketViewModel
    {
        public static Expression<Func<Ticket, AdminTicketViewModel>> FromTicket
        {
            get
            {
                return t => new AdminTicketViewModel()
                {
                    Id = t.Id,
                    FilmTitle = t.Film.Title,
                    FilmId = t.FilmId,
                    Price = t.Price,
                    ReferenceCode = t.ReferenceCode,
                    UserName = t.User.UserName,
                };
            }
        }

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Film Title")]
        public string FilmTitle { get; set; }

        [ScaffoldColumn(false)]
        public int FilmId { get; set; }

        public decimal Price { get; set; }

        [Display(Name = "Reference Code")]
        public Guid ReferenceCode { get; set; }

        public string UserName { get; set; }
    }
}