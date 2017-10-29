using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Caching;

namespace Library.Models
{
    public interface ILibraryDb : IDisposable
    {
        IQueryable<T> Query<T>() where T : class;
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Remove<T>(T entity) where T : class;
        

    }

    public class LibraryDb : DbContext, ILibraryDb
    {
        /// <summary>
        /// SImple set of data
        /// </summary>
        public DbSet<Book> Books { get; set; }

        public IQueryable<T> Query<T>() where T : class
        {
            return Set<T>();
        }

        public void Add<T>(T entity) where T : class
        {
            Set<T>().Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            Entry(entity).State = EntityState.Modified;

        }

        public void Remove<T>(T entity) where T : class
        {
            Set<T>().Remove(entity);
        }

        
    }
}