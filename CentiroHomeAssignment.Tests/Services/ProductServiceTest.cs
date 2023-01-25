using CentiroHomeAssignment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CentiroHomeAssignment.Tests.Services
{
    [TestClass]
    public class ProductServiceTest
    {
        [TestMethod]
        public async Task GetAllProducts_Returns_All_Products()
        {
            var options = new DbContextOptionsBuilder<OrderContext>()
                .UseInMemoryDatabase(databaseName: "OrderList")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new OrderContext(options))
            {
                context.Products.Add(new ProductModel { ProductNumber = "1"});
                context.Products.Add(new ProductModel { ProductNumber = "2" });

                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new OrderContext(options))
            {
                ProductService ProductService = new ProductService(context);
                List<ProductModel> orders = await ProductService.GetAllProducts();

                Assert.AreEqual(2, orders.Count);
                Assert.AreEqual("1", orders[0].ProductNumber);

                context.Products.RemoveRange(context.Products);
                context.SaveChanges();
            }
        }

        [TestMethod]
        public async Task GetByProductNumber_Returns_The_Product()
        {
            var options = new DbContextOptionsBuilder<OrderContext>()
                .UseInMemoryDatabase(databaseName: "OrderList")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new OrderContext(options))
            {
                context.Products.Add(new ProductModel { ProductNumber = "1"});
                context.Products.Add(new ProductModel { ProductNumber = "2" });

                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new OrderContext(options))
            {
                ProductService ProductService = new ProductService(context);
                ProductModel order1 = await ProductService.GetByProductNumber("1");

                Assert.AreEqual("1", order1.ProductNumber);

                context.Products.RemoveRange(context.Products);
                context.SaveChanges();
            }
        }

        [TestMethod]
        public async Task CreateNewProduct_Creates_Product()
        {
            var options = new DbContextOptionsBuilder<OrderContext>()
                .UseInMemoryDatabase(databaseName: "OrderList")
                .Options;

            ProductModel product = new ProductModel { 
                ProductNumber = "1", Name = "Product1" 
            };
            
            using (var context = new OrderContext(options))
            {
                ProductService ProductService = new ProductService(context);
                await ProductService.CreateNewProduct(product);

                ProductModel createdProduct = context.Products.Find("1");
                Assert.AreEqual("Product1", createdProduct.Name);
                

                context.Products.RemoveRange(context.Products);
                context.SaveChanges();
            }
        }
    }
}
