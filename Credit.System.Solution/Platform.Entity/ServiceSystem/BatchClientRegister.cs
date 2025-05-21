using static Platform.Entity.Enums.BatchClientRegisterEnums;

namespace Platform.Entity.ServiceSystem
{
    public class BatchClientRegister : BaseEntity
    {
        public long BatchClientRegisterId { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public BatchClientRegisterStatus Status { get; set; }
       
        public long CompanyId { get; set; }
    
    }
}
