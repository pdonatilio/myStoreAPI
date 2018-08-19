namespace MyStoreApi.Models
{
    public class OrderItem
    {
        public long orderItemID { get; set; }
        public long orderID { get; set; }
        public long productID { get; set; }
        public int quantity { get; set; }

    }
}