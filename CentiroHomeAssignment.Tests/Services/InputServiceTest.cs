using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace CentiroHomeAssignment.Tests.Services
{
    [TestClass]
    public class InputServiceTest
    {
        [TestMethod]
        public void ReadOrderAndProductsFromFile_Parses_File()
        {
            InputService inputService = new InputService();

            var path = AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin"));
            (var order, var products) = inputService.ReadOrderAndProductsFromFile(Path.Join(path, "test_data/TestOrder1.txt"), "csv");

            Assert.AreEqual(17890, order.OrderNumber);
            Assert.AreEqual(new System.DateTime(2014, 01, 25), order.OrderDate);
            Assert.AreEqual("Daniel Johansson", order.CustomerName);
            Assert.AreEqual(737268, order.CustomerNumber);
            Assert.AreEqual(4, order.OrderProducts.Count);

            Assert.AreEqual("123451324A", order.OrderProducts[0].ProductNumber);
            Assert.AreEqual(1, order.OrderProducts[0].Quantity);

            Assert.AreEqual(4, products.Count);
            Assert.AreEqual("X-Wing Starfighter", products[0].Name);
            Assert.AreEqual("Super awesome starfighter", products[0].Description);
            Assert.AreEqual(99.99, products[0].Price);
            Assert.AreEqual("Star Wars", products[0].ProductGroup);


        }
    }
}
