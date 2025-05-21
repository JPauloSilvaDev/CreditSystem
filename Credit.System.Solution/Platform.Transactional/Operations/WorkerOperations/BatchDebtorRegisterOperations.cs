using Microsoft.EntityFrameworkCore;
using Platform.Entity.ServiceSystem;
using Platform.Repository;
using static Platform.Entity.Enums.BatchDebtorRegisterEnums;

namespace Platform.Transactional.Operations.WorkerOperations
{
    public class BatchDebtorRegisterOperations
    {
        private readonly ServiceSystemConnection _serviceSystemConnection;

        public BatchDebtorRegisterOperations(ServiceSystemConnection serviceSystemConnection)
        {
            _serviceSystemConnection = serviceSystemConnection;
        }

        public async Task InsertBatchDebtorRegisterAsync(BatchDebtorRegister batchClientRegister)
        {

            batchClientRegister.Status = BatchDebtorRegisterStatus.Created;
            batchClientRegister.CreationDate = DateTime.Now;

            await _serviceSystemConnection.BatchDebtorRegister.AddAsync(batchClientRegister);
            await _serviceSystemConnection.SaveChangesAsync();

        }

        public async Task<List<BatchDebtorRegister>> GetBatchToProcessByStatusAsync(BatchDebtorRegisterStatus batchClientRegisterStatus)
        {
            return await _serviceSystemConnection.BatchDebtorRegister.Where(x => x.Status == batchClientRegisterStatus).ToListAsync();
        }

        public async Task UpdateBatchStatusAsync(long batchId, BatchDebtorRegisterStatus status)
        {

            BatchDebtorRegister batchClientRegister = await _serviceSystemConnection.BatchDebtorRegister.Where(x => x.BatchDebtorRegisterId == batchId).FirstOrDefaultAsync();

            batchClientRegister.UpdateDate = DateTime.Now;
            batchClientRegister.Status = status;

            await _serviceSystemConnection.SaveChangesAsync();

        }




    }
}
