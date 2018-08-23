namespace MyStoreApi.Models
{
    public class Order
    {
        public long orderID { get; set; }
        public int invoice { get; set; }
        public long customerID { get; set; }
        public virtual Customer customers { get; set; }
    }
}