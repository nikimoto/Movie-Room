using MoviesRoom.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MoviesRoom.Areas.Administration.ViewModels
{
    public class GridFilmViewModel
    {
        public static Expression<Func<Film, GridFilmViewModel>> FromFilm
        {
            get
            {
                return f => new GridFilmViewModel()
                {
                    Id = f.Id,
                    Title = f.Title,
                    AvailableTickets = f.AvailableTickets,
                    CategoryName = f.Category.Name,
                    CategoryId = f.Category.Id,
                    Description = f.Description,
                    PricePerTicket = f.PricePerTicket,
                    StartDate = f.StartDate
                };
            }
        }

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Title  must be lower than 25 symbols length")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Tickets are required")]
        [Range(0, 150, ErrorMessage = "Tickets must be between 0 and 150")]
        public int AvailableTickets { get; set; }

        public string CategoryName { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0, 100000, ErrorMessage = "Price must be between 0 and 100000")]
        public decimal PricePerTicket { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }
    }
}