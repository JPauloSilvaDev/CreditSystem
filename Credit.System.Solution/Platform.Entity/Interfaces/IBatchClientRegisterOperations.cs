using Platform.Entity.ServiceSystem;
using static Platform.Entity.Enums.BatchClientRegisterEnums;

namespace Platform.Entity.Interfaces
{
    public interface IBatchClientRegisterOperations
    {
        public Task InsertBatchClientRegisterAsync(BatchClientRegister batchClientRegister);
        public List<BatchClientRegister> GetBatchToProcessByStatus(BatchClientRegisterStatus batchClientRegisterStatus);
        public void UpdateBatchStatus(long batchClientRegisterId, BatchClientRegisterStatus status);
    
    }
}
