namespace Store.Data.Entities.IdentityEntities
{
    public class Address
    {
        public int AddressId { get; set; }
        public string FirstName { get; set; }   
        public string LastName { get; set; }   
        public string Street { get; set; }   
        public string City { get; set; }   
        public string State { get; set; }   
        public int ZipCode { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}