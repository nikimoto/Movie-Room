using MoviesRoom.Areas.Administration.ViewModels;
using MoviesRoom.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace MoviesRoom.ViewModels.Film
{
    public class FilmViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        
        [Required]
        [StringLength(25, ErrorMessage = "Title  must be lower than 25 symbols length")]
        public string Title { get; set; }
        
        public byte[] Image { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Tickets are required")]
        [Range(0, 150, ErrorMessage = "Tickets must be between 0 and 150")]
        public int AvailableTickets { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0, 100000, ErrorMessage = "Price must be between 0 and 100000")]
        public decimal PricePerTicket { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
       
        //[Required(ErrorMessage = "Category is required")]
        //public int CategoryId { get; set; }

        public CategoryViewModel Category { get; set; }

        public static Expression<Func<MoviesRoom.Models.Film, FilmViewModel>> FromFilm
        {
            get
            {
                return film => new FilmViewModel
                {
                    Id = film.Id,
                    Title = film.Title,
                    Image = film.Image,
                    Description = film.Description,
                    AvailableTickets = film.AvailableTickets,
                    PricePerTicket = film.PricePerTicket,
                    StartDate = film.StartDate,
                    Category = new CategoryViewModel()
                    {
                        Id = film.Category.Id,
                        Name = film.Category.Name
                    }
                };
            }
        }
    }
}