using ChruchBulletin.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Mapping
{
    public class DataContext : DbContext
    {
        private readonly IDataConfiguration _config;
        public DataContext(IDataConfiguration config)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseSqlServer(_config.GetConnectionString());
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new BulletinMap().Map(modelBuilder);
        }

        public override string ToString()
        {
            return base.ToString() + "-" + GetHashCode();
        }
    }
}
