namespace Services.GoodsReceiptAPI.Models.Dto
{
    public class SupplierDto
    {
        public int Supplier_ID { get; set; }

        public string SupplierName { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public Boolean Status { get; set; }
    }
}
