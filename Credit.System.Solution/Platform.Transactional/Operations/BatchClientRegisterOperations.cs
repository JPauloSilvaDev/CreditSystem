using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Platform.Entity.Interfaces;
using Platform.Entity.ServiceSystem;
using Platform.Repository;
using Platform.Entity.Enums;
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
            try
            {
                batchClientRegister.Status = BatchClientRegisterStatus.Created;
                batchClientRegister.CreationDate = DateTime.Now;
               
                await _serviceSystemConnection.BatchClientRegister.AddAsync(batchClientRegister);
                await _serviceSystemConnection.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
                   
        }
    
        public List<BatchClientRegister> GetBatchToProcessByStatus(BatchClientRegisterStatus batchClientRegisterStatus)
        {
            try
            {
                return _serviceSystemConnection.BatchClientRegister.Where(x => x.Status == batchClientRegisterStatus).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateBatchStatus(long batchId, BatchClientRegisterStatus status)
        {
            try
            {
                BatchClientRegister batchClientRegister = _serviceSystemConnection.BatchClientRegister.Where(x => x.BatchClientRegisterId == batchId).FirstOrDefault();

                batchClientRegister.UpdateDate = DateTime.Now;
                batchClientRegister.Status = status;

                _serviceSystemConnection.SaveChanges();

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
