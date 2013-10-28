using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoviesRoom.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage="Category is required")]
        [StringLength(20, ErrorMessage = "Title length must be lower than 20 symbols Length")]
        public string Name { get; set; }

        public virtual ICollection<Film> Films { get; set; }

        public Category()
        {
            this.Films = new HashSet<Film>();
        }
    }
}