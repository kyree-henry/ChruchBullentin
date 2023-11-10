using ChruchBulletin.DataAccess;
using Microsoft.Extensions.Configuration;

namespace ChruchBulletin.DataAccess.Test
{
    public class TestDatabaseConfiguration : IDatabaseConfiguration
    {
        private readonly IConfiguration _configuration;

        public TestDatabaseConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnectionString()
        {
            return _configuration.GetConnectionString("SqlConnectionString");
        }
    }

}