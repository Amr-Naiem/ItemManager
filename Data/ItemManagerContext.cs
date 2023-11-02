using ItemManager.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ItemManager.Data
{
	public class ItemManagerContext : DbContext
	{
		public ItemManagerContext(DbContextOptions options) : base(options)
		{
		}


        public DbSet<Item> Items { get; set; }
    }
}
