using MVC_Area.Models.Entities;

namespace MVC_Area.Models.Context.Seeds
{
    public class ProductSeedData
    {
        public static List<Product> products = new List<Product>
            {
                new Product{Id=1,ProductName="Ayakkabı",UnitPrice=5000,CategoryId=1},
                new Product{Id=2,ProductName="Laptop",UnitPrice=50000,CategoryId=2},
                new Product{Id=3,ProductName="Parfüm",UnitPrice=500,CategoryId=3},
            };
    }
}
