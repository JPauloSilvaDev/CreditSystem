using Platform.Entity.ServiceSystem;

namespace Platform.Repository
{
    public class ClientRepository : DapperConnection
    {
        public ClientRepository(IDatabaseConnection databaseConnection) : base(databaseConnection)
        {
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            const string sql = @"
                SELECT 
                    ClientId,
                    CompanyId,
                    PrimaryName,
                    SecondaryName,
                    Email,
                    Document,
                    PrimaryPhone,
                    SecondaryPhone,
                    Zipcode,
                    Street,
                    StreetNumber,
                    Observation,
                    State,
                    City,
                    Neighborhood
                FROM Client";

            return await QueryAsync<Client>(sql);
        }

        public async Task<Client> GetByIdAsync(long clientId)
        {
            const string sql = @"
                SELECT 
                    ClientId,
                    CompanyId,
                    PrimaryName,
                    SecondaryName,
                    Email,
                    Document,
                    PrimaryPhone,
                    SecondaryPhone,
                    Zipcode,
                    Street,
                    StreetNumber,
                    Observation,
                    State,
                    City,
                    Neighborhood
                FROM Client 
                WHERE ClientId = @ClientId";

            return await QueryFirstOrDefaultAsync<Client>(sql, new { ClientId = clientId });
        }

        public async Task<long> InsertAsync(Client client)
        {
            const string sql = @"
                INSERT INTO Client (
                    CompanyId,
                    PrimaryName,
                    SecondaryName,
                    Email,
                    Document,
                    PrimaryPhone,
                    SecondaryPhone,
                    Zipcode,
                    Street,
                    StreetNumber,
                    Observation,
                    State,
                    City,
                    Neighborhood
                ) VALUES (
                    @CompanyId,
                    @PrimaryName,
                    @SecondaryName,
                    @Email,
                    @Document,
                    @PrimaryPhone,
                    @SecondaryPhone,
                    @Zipcode,
                    @Street,
                    @StreetNumber,
                    @Observation,
                    @State,
                    @City,
                    @Neighborhood
                );
                SELECT CAST(SCOPE_IDENTITY() as bigint)";

            return await ExecuteScalarAsync<long>(sql, client);
        }

        public async Task<bool> UpdateAsync(Client client)
        {
            const string sql = @"
                UPDATE Client 
                SET CompanyId = @CompanyId,
                    PrimaryName = @PrimaryName,
                    SecondaryName = @SecondaryName,
                    Email = @Email,
                    Document = @Document,
                    PrimaryPhone = @PrimaryPhone,
                    SecondaryPhone = @SecondaryPhone,
                    Zipcode = @Zipcode,
                    Street = @Street,
                    StreetNumber = @StreetNumber,
                    Observation = @Observation,
                    State = @State,
                    City = @City,
                    Neighborhood = @Neighborhood
                WHERE ClientId = @ClientId";

            var affectedRows = await ExecuteAsync(sql, client);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(long clientId)
        {
            const string sql = "DELETE FROM Client WHERE ClientId = @ClientId";
            var affectedRows = await ExecuteAsync(sql, new { ClientId = clientId });
            return affectedRows > 0;
        }
    }
} 