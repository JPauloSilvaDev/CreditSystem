using static Platform.Entity.Enums.BatchDebtorRegisterEnums;

namespace Platform.Entity.ServiceSystem
{
    public class BatchDebtorRegister : BaseEntity
    {
        public long BatchDebtorRegisterId { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public BatchDebtorRegisterStatus Status { get; set; }

        public long CompanyId { get; set; }

    }
}
