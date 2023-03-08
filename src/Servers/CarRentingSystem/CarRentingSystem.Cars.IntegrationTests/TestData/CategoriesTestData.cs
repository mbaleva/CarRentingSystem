using CarRentingSystem.Cars.Data.Models;

namespace CarRentingSystem.Cars.IntegrationTests.TestData
{
    public static class CategoriesTestData
    {
        public static List<Category> GetAll() 
        {
            var list = new List<Category>();

            foreach (var item in Enumerable.Range(0, 10))
            {
                list.Add(new Category
                {
                    Id = item,
                    Name = $"Name {item}"
                });
            }
            return list;
        }
    }
}
