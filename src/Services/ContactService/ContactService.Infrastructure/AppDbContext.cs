using ContactService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
        public DbSet<Contact> Contacts{ get; set; }
        public DbSet<ContactInformation> ContactInformation { get; set; }
    }
}
