namespace MyStoreApi.Models
{
    public class OrderItems
    {
        public long orderItemID { get; set; }
        public long orderID { get; set; }
        public long productID { get; set; }
        public int quantity { get; set; }

    }
}