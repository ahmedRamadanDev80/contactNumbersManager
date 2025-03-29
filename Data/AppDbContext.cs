using contactNumbersManager.Models;
using Microsoft.EntityFrameworkCore;
namespace contactNumbersManager.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Contact> Contacts { get; set; }
    }
}
