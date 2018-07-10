using Microsoft.EntityFrameworkCore;
using MusicPortal.DAL.Entities;
using System;

namespace MusicPortal.DAL.EF {
    public class ApplicationContext : DbContext {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {
        }

        public DbSet<Artist> Artists { get; set; }

        public DbSet<Album> Albums { get; set; }

        public DbSet<Track> Tracks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {   
        }
    }
}