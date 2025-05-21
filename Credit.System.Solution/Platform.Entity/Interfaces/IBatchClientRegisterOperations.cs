using Platform.Entity.ServiceSystem;
using static Platform.Entity.Enums.BatchClientRegisterEnums;

namespace Platform.Entity.Interfaces
{
    public interface IBatchClientRegisterOperations
    {
        public Task InsertBatchClientRegisterAsync(BatchClientRegister batchClientRegister);
        public Task<List<BatchClientRegister>> GetBatchToProcessByStatusAsync(BatchClientRegisterStatus batchClientRegisterStatus);
        public Task UpdateBatchStatusAsync(long batchClientRegisterId, BatchClientRegisterStatus status);
    
    }
}
