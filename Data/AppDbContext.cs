using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Identity;
using MvcHotelApp.Models;


namespace MvcHotelApp.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<Hotel> dbhotel { get; set; }
        public DbSet<Rooms> Rooms{get;set;}
        public DbSet<RoomTypes> RoomTypes{get;set;}
        protected override void OnModelCreating(ModelBuilder modelbuilder){
            base.OnModelCreating(modelbuilder);
            //modelbuilder.Seed();

            //set on delete cascades
            // foreach (var foreignkey in modelbuilder.Model.GetEntityTypes()
            // .SelectMany(e=>e.GetForeignKeys()))
            // {
            //     foreignkey.DeleteBehaviour=DeleteBehavior.Restrict;

            // }
            
        }

        // protected override void OnModelCreating(DbModelBuilder modelBuilder){
        //     // configures one-to-many relationship
        //     modelBuilder.Entity<Hotel>()
        //         .HasRequired<Rooms>(b => b.Author)
        //         .WithMany(a => a.Books)
        //         .HasForeignKey<int>(b => b.AuthorId);
        // }
        //3.1.9
    }

   
}