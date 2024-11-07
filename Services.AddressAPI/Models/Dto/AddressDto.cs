namespace Services.AddressAPI.Models.Dto
{
    public class AddressDto
    {
        public int Address_ID { get; set; }

        public int Customer_ID { get; set; }

        public string AddressLine { get; set; }

        public string Province { get; set; }

        public string Ward { get; set; }

        public string District { get; set; }

        public string Phone { get; set; }

        public string Name { get; set; }

        public bool IsDefault { get; set; }
    }
}
