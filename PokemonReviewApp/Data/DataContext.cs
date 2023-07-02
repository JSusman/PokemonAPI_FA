using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Models;
using System.Security.Cryptography;

namespace PokemonReviewApp.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        { 
            
        }

        public DbSet<Catagory> Catagories { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Owner> Owners { get; set; }

        public DbSet<Pokemon> Pokemon { get; set; }

        public DbSet<PokemonOwner> PokemonOwners { get; set; }

        public DbSet<PokemonCatagory> PokemonCatagories { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Reviewer> Reviewers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PokemonCatagory>()
                .HasKey(pc => new { pc.PokemonId, pc.CatagoryId });
            modelBuilder.Entity<PokemonCatagory>()
                .HasOne(p => p.Pokemon)
                .WithMany(pc => pc.PokemonCatagories)
                .HasForeignKey(p => p.PokemonId);
            modelBuilder.Entity<PokemonCatagory>()
                .HasOne(p => p.Catagory)
                .WithMany(pc => pc.PokemonCatagories)
                .HasForeignKey(p => p.CatagoryId);

            modelBuilder.Entity<PokemonOwner>()
                .HasKey(po => new { po.PokemonId, po.OwnerId });
            modelBuilder.Entity<PokemonOwner>()
                .HasOne(p => p.Pokemon)
                .WithMany(po => po.PokemonOwners)
                .HasForeignKey(p => p.PokemonId);
            modelBuilder.Entity<PokemonOwner>()
                .HasOne(p => p.Owner)
                .WithMany(po => po.PokemonOwners)
                .HasForeignKey(p => p.OwnerId);




        }
    }
}
