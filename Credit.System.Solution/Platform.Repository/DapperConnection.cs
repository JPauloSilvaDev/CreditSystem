using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace Platform.Repository
{
    public abstract class DapperConnection
    {
        private readonly IDatabaseConnection _databaseConnection;

        protected DapperConnection(IDatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        protected async Task<T> ExecuteScalarAsync<T>(string sql, object parameters = null)
        {
            using (var connection = new SqlConnection(_databaseConnection.GetConnectionString()))
            {
                await connection.OpenAsync();
                return await connection.ExecuteScalarAsync<T>(sql, parameters);
            }
        }

        protected async Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters = null)
        {
            using (var connection = new SqlConnection(_databaseConnection.GetConnectionString()))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<T>(sql, parameters);
            }
        }

        protected async Task<int> ExecuteAsync(string sql, object parameters = null)
        {
            using (var connection = new SqlConnection(_databaseConnection.GetConnectionString()))
            {
                await connection.OpenAsync();
                return await connection.ExecuteAsync(sql, parameters);
            }
        }

        protected async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object parameters = null)
        {
            using (var connection = new SqlConnection(_databaseConnection.GetConnectionString()))
            {
                await connection.OpenAsync();
                return await connection.QueryFirstOrDefaultAsync<T>(sql, parameters);
            }
        }
    }
} 