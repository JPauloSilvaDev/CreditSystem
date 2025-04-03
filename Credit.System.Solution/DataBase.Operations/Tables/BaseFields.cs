using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Operations.Tables
{
    public class BaseFields
    {
        public DateTime? DeletionDate { get; set; }

        public DateTime CreationDate { get; set; }
    
        public DateTime? UpdateDate { get; set; }

    }
}
