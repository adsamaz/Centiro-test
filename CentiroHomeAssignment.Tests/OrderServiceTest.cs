using CentiroHomeAssignment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CentiroHomeAssignment.Tests
{
    [TestClass]
    public class OrderServiceTest
    {
        [TestMethod]
        public async Task GetAllOrders_Returns_All_Orders()
        {
            var options = new DbContextOptionsBuilder<OrderContext>()
                .UseInMemoryDatabase(databaseName: "OrderList")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new OrderContext(options))
            {
                context.Orders.Add(new OrderModel { OrderNumber = 1, OrderDate = new System.DateTime(2023, 01, 22), CustomerName = "Customer1", CustomerNumber = 123 });
                context.Orders.Add(new OrderModel { OrderNumber = 2, OrderProducts = new List<OrderProductModel> { new OrderProductModel { ProductNumber = "Prod12345" } } });

                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new OrderContext(options))
            {
                OrderService orderService = new OrderService(context);
                List<OrderModel> orders = await orderService.GetAllOrders();

                Assert.AreEqual(2, orders.Count);
                Assert.AreEqual(new System.DateTime(2023, 01, 22), orders[0].OrderDate);
                Assert.AreEqual("Customer1", orders[0].CustomerName);
                Assert.AreEqual(123, orders[0].CustomerNumber);
                Assert.AreEqual("Prod12345", orders[1].OrderProducts[0].ProductNumber);

                context.Orders.RemoveRange(context.Orders);
                context.SaveChanges();
            }
        }

        [TestMethod]
        public async Task GetByOrderNumber_Returns_The_Order()
        {
            var options = new DbContextOptionsBuilder<OrderContext>()
                .UseInMemoryDatabase(databaseName: "OrderList")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new OrderContext(options))
            {
                context.Orders.Add(new OrderModel { OrderNumber = 1, OrderDate = new System.DateTime(2023, 01, 22), CustomerName = "Customer1", CustomerNumber = 123 });
                context.Orders.Add(new OrderModel { OrderNumber = 2, OrderProducts = new List<OrderProductModel> { new OrderProductModel { ProductNumber = "Prod12345" } } });

                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new OrderContext(options))
            {
                OrderService orderService = new OrderService(context);
                OrderModel order1 = await orderService.GetByOrderNumber(1);

                Assert.AreEqual(new System.DateTime(2023, 01, 22), order1.OrderDate);
                Assert.AreEqual("Customer1", order1.CustomerName);
                Assert.AreEqual(123, order1.CustomerNumber);

                OrderModel order2 = await orderService.GetByOrderNumber(2);
                Assert.AreEqual("Prod12345", order2.OrderProducts[0].ProductNumber);

                context.Orders.RemoveRange(context.Orders);
                context.SaveChanges();
            }
        }

        [TestMethod]
        public async Task CreateNewOrder_Creates_Order()
        {
            var options = new DbContextOptionsBuilder<OrderContext>()
                .UseInMemoryDatabase(databaseName: "OrderList")
                .Options;

            OrderModel order = new OrderModel { 
                OrderNumber = 1, OrderDate = new System.DateTime(2023, 01, 22), 
                CustomerName = "Customer1", CustomerNumber = 123, 
                OrderProducts = new List<OrderProductModel> { new OrderProductModel { ProductNumber = "Prod12345" } } 
            };
            
            using (var context = new OrderContext(options))
            {
                OrderService orderService = new OrderService(context);
                await orderService.CreateNewOrder(order);

                OrderModel createdOrder = context.Orders.Find(1);
                Assert.AreEqual(new System.DateTime(2023, 01, 22), createdOrder.OrderDate);
                Assert.AreEqual("Customer1", createdOrder.CustomerName);
                Assert.AreEqual(123, createdOrder.CustomerNumber);
                Assert.AreEqual("Prod12345", createdOrder.OrderProducts[0].ProductNumber);

                context.Orders.RemoveRange(context.Orders);
                context.SaveChanges();
            }
        }
    }
}
