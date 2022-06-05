using Microsoft.EntityFrameworkCore;
using TechnicalTask.ViewModel;

namespace TechnicalTask.Data
{
    public class DataContext : DbContext
    {
        
            public DataContext(DbContextOptions<DataContext> options)
                : base(options)
            {
        }

        public DbSet<Countries> Countries { get; set; }
        public DbSet<Populations> Populations { get; set; }
    }
}
