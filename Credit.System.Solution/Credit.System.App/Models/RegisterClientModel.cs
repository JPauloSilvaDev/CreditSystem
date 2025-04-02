namespace Credit.System.App.Models
{
    public class RegisterClientModel
    {
        public long ClientId { get; set; }

        public string Name { get; set; }

        public string? ZipCode { get; set; }
        
        public string? Address{ get; set; }
        
        public short? AddressNumber { get; set; }
       
        public string Email { get; set; }
       
        public string PhoneNumber { get; set; }
        
        public string State { get; set; }

        public string Neighborhood { get; set; }

    }
}
