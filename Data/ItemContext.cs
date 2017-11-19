using Microsoft.EntityFrameworkCore;

namespace PersistentUnreal.Data
{
    public class ItemContext : DbContext
    {
        public ItemContext(DbContextOptions<ItemContext> options)
            : base(options)
        {
        }

        public DbSet<ItemRecord> Items { get; set; }
    }
}
