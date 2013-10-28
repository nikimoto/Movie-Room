using Microsoft.AspNet.Identity.EntityFramework;
using MoviesRoom.Models;
using System;
using System.Data.Entity;
using System.Linq;

namespace MoviesRoom.Data
{
    public class MoviesContext : IdentityDbContext<ExtendedUser, UserClaim, UserSecret, UserLogin, Role, UserRole, Token, UserManagement>
    {
        public MoviesContext() : base("MoviesRoom")
        {
        }

        public IDbSet<Comment> Comments { get; set; }

        public IDbSet<Film> Films { get; set; }

        public IDbSet<Category> Categories { get; set; }

        public IDbSet<Ticket> Tickets { get; set; }
    }
}