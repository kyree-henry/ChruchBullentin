using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;

namespace ChruchBulletin.DataAccess.Test
{
    public class SqlExecuter
    {
        private readonly DatabaseFacade _facade;

        public SqlExecuter(DatabaseFacade facade)
        {
            _facade = facade;
        }

        [SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        public void ExecuteSql(string commandText, Action<DbDataReader> readerAction)
        {
            var connection = _facade.GetDbConnection();
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText =
                    commandText;
                var reader = command.ExecuteReader();
                while (reader.Read()) readerAction(reader);
            }

            connection.Close();
        }
    }
}
