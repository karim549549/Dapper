using Dapper;
using Event_Management_Task.Models;
using Event_Management_Task.Services;
using Event_Management_Task.Utilities;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using static Dapper.SqlMapper;

namespace Event_Management_Task.Repositories
{
    public class BaseRepository

    {
        private readonly SqlConnectionFactory ConnectionFactory;
        
        public BaseRepository(SqlConnectionFactory factory)
        {
            ConnectionFactory = factory;
        }
        public async Task<IEnumerable<Entity>> BulkExecuteProcedureAsync<Entity>(string procedureName,
            DynamicParameters parameters = null)
        {
            try
            {
                using var connection = ConnectionFactory.Create();
                var result = await connection.QueryAsync<Entity>(procedureName, parameters,
                                    commandType: CommandType.StoredProcedure);
                return result;
            }
            catch
            {
                throw new Exception("An error occurred while executing the stored procedure.");
            }
        }
        public async Task<Entity> ExecuteProcedureAsync<Entity>(string procedureName,
       DynamicParameters parameters = null)
        {
            try
            {
                using var connection = ConnectionFactory.Create();
                var result = await connection.QueryFirstOrDefaultAsync<Entity>(procedureName, parameters,
                                    commandType: CommandType.StoredProcedure);
                return result;
            }
            catch
            {
                throw new Exception("An error occurred while executing the stored procedure.");
            }
        }
    }
}
