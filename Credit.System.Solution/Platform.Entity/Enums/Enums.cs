namespace Platform.Entity.Enums
{
    public static class BatchClientRegisterEnums
    {
        
        public enum BatchClientRegisterStatus
        {
            Created = 0,
            Processing = 1,
            PartiallyProcessed = 2,
            Processed = 3,
            Error = 4
        }
    
    }

    public static class BillEnums
    {
        public enum BillStatus
        {
            Unknown = 0,
            Pending = 1,
            Settled = 2,
            PartiallySettled = 3,
            Ongoing = 4
        }

    }

    public static class BatchDebtorRegisterEnums
    {
        public enum BatchDebtorRegisterStatus
        {
            Created = 0,
            Processing = 1,
            PartiallyProcessed = 2,
            Processed = 3,
            Error = 4
        }
    }

}
