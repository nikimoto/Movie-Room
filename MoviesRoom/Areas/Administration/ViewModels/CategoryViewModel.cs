using MoviesRoom.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MoviesRoom.Areas.Administration.ViewModels
{
    public class CategoryViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [StringLength(20, ErrorMessage = "Title length must be lower than 20 symbols Length")]
        public string Name { get; set; }

        public static Expression<Func<MoviesRoom.Models.Category, CategoryViewModel>> FromCategory
        {
            get
            {
                return c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                };
            }
        }

        public  Category CreateCategory()
        {
            return new Category()
            {
                Id = this.Id,
                Name = this.Name
            };
        }
    }
}