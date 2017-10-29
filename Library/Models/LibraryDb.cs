using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class LibraryDb : DbContext
    {
        public DbSet<Book> Books { get; set; }
    }
}