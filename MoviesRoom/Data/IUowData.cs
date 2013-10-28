using MoviesRoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesRoom.Data
{
    public interface IUowData : IDisposable
    {
        IRepository<Film> Films { get; }

        IRepository<Category> Categories { get; }

        IRepository<ExtendedUser> Users { get; }

        IRepository<Ticket> Tickets { get; }

        IRepository<Comment> Comments { get; }

        int SaveChanges();
    }
}