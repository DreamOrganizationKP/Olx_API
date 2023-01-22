using Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class AppDbContext : IdentityDbContext<User>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<User>()
            //    .Ignore(u => u.UserName)
            //    .Ignore(u => u.NormalizedUserName);
            //builder.Entity<User>().ToTable("AspNetUsers");
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketPhoto> TicketsPhotos { get; set; }
    }
}
