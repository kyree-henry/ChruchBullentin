using DataAccess;
using Microsoft.Extensions.Configuration;

namespace ChruchBulletin.DataAccess.Test
{
    public class TestDataConfiguration : IDataConfiguration
    {
        public string GetConnectionString()
        {
            return "server=(LocalDb)\\MSSQLLocalDB;database=ChruchBullentin;integrated Security=true;";
        }
    }

}