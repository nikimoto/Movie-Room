using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesRoom.Models
{
    // You can add profile data for the user by adding more properties to your User class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ExtendedUser : User
    {
        [Required(ErrorMessage = "First Name is required")]
        [StringLength(25, ErrorMessage = "First Name must be lower than 25 symbols length")]
          
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(25, ErrorMessage = "Last Name must be lower than 25 symbols length")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^\w+[\w-\.]*\@\w+((-\w+)|(\w*))\.[a-z]{2,3}$", ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
        
        public ExtendedUser()
        {
            this.Tickets = new HashSet<Ticket>();
        }

        public ExtendedUser(string userName) : base(userName)
        {
        }
    }
}