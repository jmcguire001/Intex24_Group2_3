using Microsoft.EntityFrameworkCore;

namespace Intex24_Group2_3.Models
{
    public class ShoppingContext : DbContext
    {
        // Constructor. Inherit base options; provide way to enter your own options
        public ShoppingContext(DbContextOptions<ShoppingContext> options) : base(options)
        {
        }

        // Create a public set that consists of instances of Project. Saves individual projects into Sets (aka table called 'Projects')
        public DbSet<Project> Projects { get; set; }
    }
}
