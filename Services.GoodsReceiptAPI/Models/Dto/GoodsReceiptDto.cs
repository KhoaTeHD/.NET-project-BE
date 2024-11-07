namespace Services.GoodsReceiptAPI.Models.Dto
{
    public class GoodsReceiptDto
    {
        public int Goo_ID { get; set; }
        public int Supplier_ID { get; set; }
        public DateTime Datetime { get; set; }
        public decimal Total { get; set; }

        // Danh sách chi tiết GoodsReceipt
        public List<DetailGoodsReceiptDto> DetailGoodsReceipts { get; set; }
    }
}
