using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MoviesRoom.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public DateTime DateOfPurchase { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0, 100000, ErrorMessage = "Price must be between 0 and 100000")]
        public decimal Price { get; set; }

        [Required]
        public Guid ReferenceCode { get; set; }
     
        [Required(ErrorMessage = "User is required")]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ExtendedUser User { get; set; }
     
        [Required(ErrorMessage = "Film is required")]
        public int FilmId { get; set; }

        [ForeignKey("FilmId")]
        public virtual Film Film { get; set; }
    }
}