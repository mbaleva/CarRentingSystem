using CarRentingSystem.Cars.Controllers;
using CarRentingSystem.Cars.IntegrationTests.TestData;
using MyTested.AspNetCore.Mvc;

namespace CarRentingSystem.Cars.IntegrationTests
{
    public class CategoriesControllerTests
    {
        [Fact]
        public void Test1()
        {
            MyMvc.Controller<CategoriesController>()
                .WithData(CategoriesTestData.GetAll())
                .Calling(action => action.All())
                .ShouldReturn()
                .Ok();
        }
    }
}