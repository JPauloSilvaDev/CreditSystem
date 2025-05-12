using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Platform.Entity.ServiceSystem;
using static Platform.Entity.Enums.Enums;

namespace Platform.Entity.Interfaces
{
    public interface IBatchClientRegisterOperations
    {
        public void Insert(BatchClientRegister batchClientRegister);
        public List<BatchClientRegister> GetBatchToProcessByStatus(BatchClientRegisterStatus batchClientRegisterStatus);
        public void UpdateBatchStatus(long batchClientRegisterId, BatchClientRegisterStatus status);
    
    }
}
