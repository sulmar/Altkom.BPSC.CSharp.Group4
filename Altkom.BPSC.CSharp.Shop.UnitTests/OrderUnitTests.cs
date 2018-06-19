using System;
using Altkom.BPSC.CSharp.Shop.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Altkom.BPSC.CSharp.Shop.UnitTests
{
    [TestClass]
    public class OrderUnitTests
    {
        [TestMethod]
        public void CreateOrderTest()
        {
            // Arrange
            Customer customer = new Customer("Marcin", "Sulecki");

            // Acts
            Order order = new Order("ZAM 001/2018", customer);

            // Asserts
            Assert.AreEqual("ZAM 001/2018", order.OrderNumber);
            Assert.IsNotNull(order.CreateDate, "Brak daty utworzenia");
            Assert.IsNotNull(order.Details, "Puste pozycje");
            Assert.IsNotNull(order.Customer, "Pusty kontrahent");

        }

        [TestMethod]
        public void CreateOrderDetailTest()
        {
            // Arrange
            Product product = new Product("Monitor", 15);

            // Acts
            OrderDetail orderDetail = new OrderDetail(product, 5);

            // Asserts
            Assert.AreEqual(5, orderDetail.Quantity);
            Assert.AreEqual(product.UnitPrice, orderDetail.UnitPrice);

        }

        [TestMethod]
        public void AmountOrderDetailTest()
        {
            // Arrange
            Product product = new Product("Monitor", 15);

            // Acts
            OrderDetail orderDetail = new OrderDetail(product, 5);

            // Asserts
            Assert.AreEqual(product.UnitPrice, orderDetail.UnitPrice);
            Assert.AreEqual(orderDetail.UnitPrice * orderDetail.Quantity, orderDetail.Amount);

        }

        [DataTestMethod]
        [DataRow(15, 5, 75)]
        [DataRow(10, 1, 10)]
        [DataRow(0, 0, 0)]
        public void MultipleAmountOrderDetailTest(int unitPrice, int quantity, int amount)
        {
            // Arrange
            Product product = new Product("Monitor", unitPrice);

            // Acts
            OrderDetail orderDetail = new OrderDetail(product, quantity);

            // Asserts
            Assert.AreEqual(product.UnitPrice, orderDetail.UnitPrice);
            Assert.AreEqual(amount, orderDetail.Amount);

        }

        [TestMethod]
        public void GroupByTest()
        {
            // Arrange
            Product product1 = new Product("Monitor", 1000);
            Product product2 = new Product("Myszka", 100);
            Service service1 = new Service("Naprawa drukarki", 200, 1);
            Service service2 = new Service("Czyszczenie komputera", 100, 1);
            Service service3 = new Service("Testy", 100, 1);
            Customer customer = new Customer("Marcin", "Sulecki");

            // Acts
            Order order = new Order("ZAM 001/2018", customer);

            order.Details.Add(new OrderDetail(product1, 10));
            order.Details.Add(new OrderDetail(product2, 10));
            order.Details.Add(new OrderDetail(service1, 1));
            order.Details.Add(new OrderDetail(service2, 1));
            order.Details.Add(new OrderDetail(service3, 1));

            var query = order.Details
                .GroupBy(d => d.Item.GetType())
                .Select(g => new { Typ = g.Key, Qty = g.Count() })
                .ToList();

            // zła praktyka
            //if (order.Details.Count()>0)
            //{

            //}

            if (order.Details.Any())
            {

            }
                
            var person = new
            {
                FirstName = "Marcin",
                LastName = "Sulecki",
                Age = 18
            };




        }
    


    }
}
