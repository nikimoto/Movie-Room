using MoviesRoom.Areas.Administration.ViewModels;
using MoviesRoom.Models;
using MoviesRoom.ViewModels.Comment;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Linq;

namespace MoviesRoom.ViewModels.Film
{
    public class FullFilmViewModel : FilmViewModel
    {
        public ICollection<CommentViewModel> Comments { get; set; }

        public static new Expression<Func<MoviesRoom.Models.Film, FullFilmViewModel>> FromFilm
        {
            get 
            {
                return film => new FullFilmViewModel 
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
                    },
                    Comments = film.Comments
                                   .AsQueryable()
                                   .Select(CommentViewModel.FromComment)
                                   .ToList()
                };
            }
        }

        public MoviesRoom.Models.Film CreateFilm()
        {
            return new MoviesRoom.Models.Film()
            {
                Id = this.Id,
                Title = this.Title,
                Image = this.Image,
                Description = this.Description,
                AvailableTickets = this.AvailableTickets,
                PricePerTicket = this.PricePerTicket,
                StartDate = this.StartDate,
                CategoryId = this.Category.Id
            };
        }
    }
}