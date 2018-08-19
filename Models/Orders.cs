namespace MyStoreApi.Models
{
    public class Orders
    {
        public long orderID { get; set; }
        public long customerID { get; set; }
        public int invoice { get; set; }
    }
}