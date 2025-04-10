using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Entity.Enums
{
    public static class Enums
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
}
