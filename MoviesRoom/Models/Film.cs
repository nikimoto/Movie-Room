using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace MoviesRoom.Models
{
    public class Film
    {
        public Film()
        {
            this.Comments = new HashSet<Comment>();
        }

        [Key]
        public int Id { get; set; }
        
        public string Title { get; set; }
        
        public byte[] Image { get; set; }

        [Column(TypeName = "Text")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Tickets are required")]
        [Range(0, 150, ErrorMessage = "Tickets must be between 0 and 150")]
        public int AvailableTickets { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0, 100000, ErrorMessage = "Price must be between 0 and 100000")]
        public decimal PricePerTicket { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public DateTime StartDate { get; set; }
       
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}