using Microsoft.EntityFrameworkCore;
using Projur.Models;

namespace Projur.BackEnd.Projur.Infrastructure.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :
            base(options)
        { }

        public DbSet<Usuarios> Pagamentos { get; set; }
    }
}
