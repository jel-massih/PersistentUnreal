using Microsoft.EntityFrameworkCore;

namespace PersistentUnreal.Data
{
    public class PUAccountDbContext : DbContext
    {
        public PUAccountDbContext(DbContextOptions<PUAccountDbContext> options)
            : base(options)
        {
        }

        public DbSet<PUAccountRecord> Accounts { get; set; }
    }

}
