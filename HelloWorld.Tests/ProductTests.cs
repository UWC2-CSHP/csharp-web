using NUnit.Framework;
using HelloWorld.Models;
using HelloWorld.Controllers;
using Moq;
using System.Linq; // ADDED

namespace HelloWorld.Tests
{
    public class ProductTests
    {
        [Test]
        public void TestMethodWithFakeClass()
        {
            // Step 1: Get the data : Arrange
            var controller = new HomeController(null, new MyJsonSettings(), new FakeProductRepository());

            // Step 2: Get the Results : Act
            var result = controller.Products();

            // Step 3: Verity and Assert
            var products = (Product[])((Microsoft.AspNetCore.Mvc.ViewResult)(result)).Model;
            Assert.AreEqual(5, products.Length, "Length is invalid");

            //3 products with price > $10 and 2 products with price < $10.
            Assert.GreaterOrEqual(products.Where(t => t.Price > 10).Count(), 3);
            Assert.GreaterOrEqual(products.Where(t => t.Price < 10).Count(), 2);
        }


        [Test] //using the Moq
        public void TestMethodWithMoq()
        {
            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository
                .SetupGet(t => t.Products)
                .Returns(() =>
                {
                    return new Product[] {
                        new Product{ Name="Baseball", Price=11},
                        new Product{ Name="Football", Price=8},
                        new Product{ Name="Tennis ball", Price=13} ,
                        new Product{ Name="Golf ball",Price=3},
                        new Product{Name="Ping pong ball", Price=12}
                        };
                });

            // Arrange
            var controller = new HomeController(
                null,
                new MyJsonSettings(),
                mockProductRepository.Object);
            // Act
            var result = controller.Products();

            // Assert
            var products = (Product[])((Microsoft.AspNetCore.Mvc.ViewResult)(result)).Model;
            Assert.AreEqual(5, products.Length, "Length is invalid");

            //3 products with price > $10 and 2 products with price < $10.
            Assert.GreaterOrEqual(products.Where(t => t.Price > 10).Count(), 3);
            Assert.GreaterOrEqual(products.Where(t => t.Price < 10).Count(), 2);
        }
    }
}

