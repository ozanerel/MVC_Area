using MVC_Area.Models.Entities;

namespace MVC_Area.Models.Context.Seeds
{
    public class CategorySeedData
    {
        public static List<Category> categories = new List<Category>
            {
                new Category{Id=1,CategoryName="Giyim",Description="Giyim Ürünleri"},
                new Category{Id=2,CategoryName="Teknoloji", Description="Teknoloji Ürünleri"},
                new Category{Id=3,CategoryName="Kozmetik",Description="Kozmetik Ürünleri"}
            };
    }
}
