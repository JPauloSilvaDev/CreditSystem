using Microsoft.EntityFrameworkCore;
using Platform.Entity.Interfaces;
using Platform.Entity.ServiceSystem;
using Platform.Repository;
using static Platform.Entity.Enums.BatchClientRegisterEnums;

namespace Platform.Transactional.Operations
{
    public class BatchClientRegisterOperations : IBatchClientRegisterOperations
    {
        private readonly ServiceSystemConnection _serviceSystemConnection;

        public BatchClientRegisterOperations(ServiceSystemConnection serviceSystemConnection)
        {
            _serviceSystemConnection = serviceSystemConnection;
        }

        public async Task InsertBatchClientRegisterAsync(BatchClientRegister batchClientRegister)
        {

            batchClientRegister.Status = BatchClientRegisterStatus.Created;
            batchClientRegister.CreationDate = DateTime.Now;

            await _serviceSystemConnection.BatchClientRegister.AddAsync(batchClientRegister);
            await _serviceSystemConnection.SaveChangesAsync();

        }

        public async Task<List<BatchClientRegister>> GetBatchToProcessByStatusAsync(BatchClientRegisterStatus batchClientRegisterStatus)
        {
            return await _serviceSystemConnection.BatchClientRegister.Where(x => x.Status == batchClientRegisterStatus).ToListAsync();
        }

        public async Task UpdateBatchStatusAsync(long batchId, BatchClientRegisterStatus status)
        {

            BatchClientRegister batchClientRegister = _serviceSystemConnection.BatchClientRegister.Where(x => x.BatchClientRegisterId == batchId).FirstOrDefault();

            batchClientRegister.UpdateDate = DateTime.Now;
            batchClientRegister.Status = status;

          await  _serviceSystemConnection.SaveChangesAsync();

        }
    }
}