using MoviesRoom.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoviesRoom.ViewModels.Ticket
{
    public class TicketViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public DateTime DateOfPurchase { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0, 100000, ErrorMessage = "Price must be between 0 and 100000")]
        public decimal Price { get; set; }

        [Required]
        public Guid ReferenceCode { get; set; }

        //[Required(ErrorMessage = "User is required")]
        //public string UserId { get; set; }

        public ExtendedUser User { get; set; }

        //[Required(ErrorMessage = "Film is required")]
        //public int FilmId { get; set; }

        public virtual MoviesRoom.Models.Film Film { get; set; }
    }
}