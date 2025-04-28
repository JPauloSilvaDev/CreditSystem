using Platform.Entity;

namespace Platform.Repository
{
    public class ExampleRepository : DapperConnection
    {
        public ExampleRepository(IDatabaseConnection databaseConnection) : base(databaseConnection)
        {
        }

        public async Task<IEnumerable<ExampleEntity>> GetAllAsync()
        {
            const string sql = "SELECT * FROM ExampleTable";
            return await QueryAsync<ExampleEntity>(sql);
        }

        public async Task<ExampleEntity> GetByIdAsync(int id)
        {
            const string sql = "SELECT * FROM ExampleTable WHERE Id = @Id";
            return await QueryFirstOrDefaultAsync<ExampleEntity>(sql, new { Id = id });
        }

        public async Task<int> InsertAsync(ExampleEntity entity)
        {
            const string sql = @"
                INSERT INTO ExampleTable (Name, Description)
                VALUES (@Name, @Description);
                SELECT CAST(SCOPE_IDENTITY() as int)";

            return await ExecuteScalarAsync<int>(sql, entity);
        }

        public async Task<bool> UpdateAsync(ExampleEntity entity)
        {
            const string sql = @"
                UPDATE ExampleTable 
                SET Name = @Name, 
                    Description = @Description
                WHERE Id = @Id";

            var affectedRows = await ExecuteAsync(sql, entity);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            const string sql = "DELETE FROM ExampleTable WHERE Id = @Id";
            var affectedRows = await ExecuteAsync(sql, new { Id = id });
            return affectedRows > 0;
        }
    }
} 