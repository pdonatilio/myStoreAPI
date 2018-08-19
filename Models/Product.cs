namespace MyStoreApi.Models
{
    public class Product
    {
        public long productID { get; set; }
        public string description { get; set; }
        public decimal unitPrice { get; set; }
        public int unitInStock { get; set; }
    }
}