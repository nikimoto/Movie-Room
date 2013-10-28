using MoviesRoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoviesRoom.Data
{
    public class UowData : IUowData
    {
        private readonly MoviesContext context;
        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public UowData() : this(new MoviesContext())
        {
        }

        public UowData(MoviesContext context)
        {
            this.context = context;
        }

        public IRepository<Comment> Comments
        {
            get
            {
                return this.GetRepository<Comment>();
            }
        }

        public IRepository<Film> Films
        {
            get
            {
                return this.GetRepository<Film>();
            }
        }

        public IRepository<Category> Categories
        {
            get
            {
                return this.GetRepository<Category>();
            }
        }

        public IRepository<Ticket> Tickets
        {
            get
            {
                return this.GetRepository<Ticket>();
            }
        }

        public IRepository<ExtendedUser> Users
        {
            get
            {
                return this.GetRepository<ExtendedUser>();
            }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        public void Dispose()
        {
            if (this.context != null)
            {
                this.context.Dispose();
            }
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(GenericRepository<T>);

                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }
    }
}