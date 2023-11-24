//It is the basic syntax or basic classes that has to be configured
//in this way in order to use entity framework core.

// For creating database based on the connection string created in appsettings.json,
// we have to write "update-database" in the Package Manager Console.

// Anything that we have to do with our database is inside application.DB Context,
// because that is where we have. DB Context, which uses entity framework core.


// In Tier-Architecture -> That will be Bulky.Models but Bulky.Models is not accessible here so we will have to right-click here.
// Add project reference and we want to add models and utility project. With that
using E_commerce.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace E_commerce.DataAccess.Data
{
    // That DB Context class is basically the root class of entity framework
    // core using which we will be accessing entity framework.
    public class ApplicationDbContext : DbContext
    {
        // we will get that connection string as a parameter in constructor
        // as DB context option and that we will be passing on to the base class.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>  options) : base(options) 
        { 
        }
        // Using Dbset to create tables
        // Use "add-migration {name}" in PMC to create table
        // then "update-database" in PMC again to apply unapplied migrations in Migration Folder
        // it will notice unapplied migrations in db.__EFMigrationsHistory table in SSMS
        public DbSet<Category>  Categories { get; set; }

        // to seed some entities in your database.
        // Whenever updating  database, needed to add a migration.
        // add-migration SeedCategoryTable, then update-database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Sci-Fi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Horror", DisplayOrder = 3 }
                );
        }
    }
}
