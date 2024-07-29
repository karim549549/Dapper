using Microsoft.Data.SqlClient;

namespace Event_Management_Task.Services
{
    public class SqlConnectionFactory
    {
        private readonly  string _connectionString;
        public SqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        public SqlConnection Create()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
