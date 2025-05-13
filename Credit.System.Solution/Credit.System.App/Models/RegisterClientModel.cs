namespace Credit.System.App.Models
{
    public class RegisterClientModel
    {

        public long CompanyId { get; set; }
        
        public long ClientId { get; set; }

        public string PrimaryName { get; set; }

        public string? SecondaryName { get; set; }

        public string Email { get; set; }

        public string Document { get; set; }
        
        public string PrimaryPhone { get; set; }

        public string? SecondaryPhone { get; set; }

        public string ZipCode { get; set; }

        public string Street { get; set; }

        public short StreetNumber { get; set; }

        public string? Observation { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Neighborhood { get; set; }

    }
}
