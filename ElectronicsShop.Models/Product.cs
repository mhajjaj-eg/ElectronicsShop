namespace ElectronicsShop.Models
{
    public class ProductCategory
    {
        public int ProductCategoryId { get; set; }
        public string TypeNameAr { get; set; }
        public string TypeNameEn { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
    public class Product
    {
        public int ProductId { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }
        public double OriginalPrice { get; set; }
        public int QuantityInStock { get; set; }
        public int Discount { get; set; }
        public int DiscountOfTwo { get; set; }
        public int ProductCategoryId { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual ICollection<ProductOrder> ProductOrders { get; set; }
    }
    public class ProductOrder
    {
        public int ProductOrderId { get; set; }
        public virtual ApplicationUser Client { get; set; }
        public virtual Product Product { get; set; }
        public int QuantityOrdered { get; set; }
    }
}
