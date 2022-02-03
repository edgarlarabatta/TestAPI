using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Data;
using System.Data.SqlClient;


namespace Test.Data.DataContext
{

    public class Connection : IConnection
    {
        private readonly IConfiguration _configuration;
        public Connection(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection GetConnection
        {
            get
            {
                var sqlConnection = new NpgsqlConnection();

                
                if (sqlConnection == null)
                    return null;

                sqlConnection.ConnectionString = _configuration.GetConnectionString("MainConnection");
                sqlConnection.Open();
                return sqlConnection;
            }
        }
    }

    public interface IConnection
    {
        IDbConnection GetConnection { get; }
    }
}
