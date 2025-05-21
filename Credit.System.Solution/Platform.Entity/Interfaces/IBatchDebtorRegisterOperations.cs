using Platform.Entity.ServiceSystem;
using static Platform.Entity.Enums.BatchDebtorRegisterEnums;

namespace Platform.Entity.Interfaces
{
    public interface IBatchDebtorRegisterOperations
    {
        public Task InsertBatchDebtorRegisterAsync(BatchDebtorRegister batchClientRegister);
        public Task<List<BatchDebtorRegister>> GetBatchToProcessByStatusAsync(BatchDebtorRegisterStatus batchClientRegisterStatus);
        public Task UpdateBatchStatusAsync(long batchClientRegisterId, BatchDebtorRegisterStatus status);
   
    }
}
