namespace FabioApi.Models
{
    public class CustomerContract
    {
        public long id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Address { get; set; }
        public int? Phone { get; set; }
    }
}

