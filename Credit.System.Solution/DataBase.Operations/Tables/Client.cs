using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Operations.Tables
{
    public class Client: BaseFields
    {
        [Column("ID")]
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Document { get; set; }
        public string PrimaryPhone { get; set; }
        public string SecondPhone { get;set; }

        // ADRESS
        
        public string Zipcode { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string Observation { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Neighborhood { get; set; }

        

    }
}
