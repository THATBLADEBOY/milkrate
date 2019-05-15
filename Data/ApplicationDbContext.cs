using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using milkrate.Models;

namespace milkrate.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<milkrate.Models.Piece> Piece { get; set; }
        public DbSet<milkrate.Models.UserPiece> UserPiece { get; set; }
        public DbSet<milkrate.Models.Condition> Condition { get; set; }
        public DbSet<milkrate.Models.TrackedValue> TrackedValue {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().HasMany(a => a.UserPieces).WithOne(u => u.User).OnDelete(DeleteBehavior.Restrict);
        }
    }

    
}