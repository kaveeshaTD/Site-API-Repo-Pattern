using Microsoft.EntityFrameworkCore;
using SiteAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteAPI.Infrastructer.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions <AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<User> users  { get; set; }
        public DbSet<Properties> properties  { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(a => a.User_Id);
            });

            modelBuilder.Entity<Properties>(entity =>
            {
                entity.HasKey(a => a.Propertie_Id);

                entity.HasOne(a=>a.User)
                .WithMany(b=>b.Properties)
                .HasForeignKey(a=>a.User_Id)
                .OnDelete(DeleteBehavior.NoAction);

            });
        }
    }
}
