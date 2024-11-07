namespace Services.CustomerAPI.Models.Dto
{
    public class CustomerDto
    {
        public int Cus_Id { get; set; }

        public string Cus_Name { get; set; }

        public string Cus_Avatar { get; set; }

        public string Cus_Email { get; set; }

        public string Cus_Phone { get; set; }

        public string Cus_Password { get; set; }

        public string Cus_Gender { get; set; }

        public DateTime Cus_Birthday { get; set; }

        public Boolean Cus_Status { get; set; }
    }
}
