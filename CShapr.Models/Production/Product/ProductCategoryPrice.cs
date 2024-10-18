namespace CShapr.Models.Production.Product
{
    public class ProductCategoryPrice
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string ProductNumber { get; set; }
        public string? Color { get; set; }
        public decimal StandardCost { get; set; }
        public decimal ListPrice { get; set; }
        public string Size { get; set; }
        public string? SizeUnitMeasureCode { get; set; }
        public int? ProductCategoryID { get; set; }
        public string ProductCategory { get; set; }
        public int? ProductSubcategoryID { get; set; }
        public string ProductSubcategory { get; set; }
    }
}
