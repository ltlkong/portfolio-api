using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioDb.Models
{
    public class PortfolioDbContext : DbContext
    {
        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<View> Views { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Drawing> Drawings { get; set; }
        public PortfolioDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
