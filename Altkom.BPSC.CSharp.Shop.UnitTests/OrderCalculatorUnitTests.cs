using System;
using Altkom.BPSC.CSharp.Shop.Models;
using Altkom.BPSC.CSharp.Shop.OrderCalculators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Altkom.BPSC.CSharp.Shop.UnitTests
{
    [TestClass]
    public class OrderCalculatorUnitTests
    {
        [TestMethod]
        public void Calculate2DiscountTest()
        {
            // Arrange
            ICanDiscountStrategy canDiscountStrategy = new HappyHoursCanDiscountStrategy(
                TimeSpan.Parse("12:30"),
                TimeSpan.Parse("14:00"));

            IDiscountStrategy discountStrategy = new FixedDiscountStrategy(5);

            OrderCalculator orderCalculator = new OrderCalculator(canDiscountStrategy, discountStrategy);

            Order order = new Order("ZAM 001/2018", new Customer("M", "S"));
            order.CreateDate = DateTime.Parse("2018-06-19 12:30");

            Product product1 = new Product("Monitor", 1000);
            order.Details.Add(new OrderDetail(product1, 10));

            // Acts
            decimal discount = orderCalculator.CalculateDiscount(order);

            // Asserts
            Assert.AreEqual(5, discount);


        }


        [TestMethod]
        public void CalculateDiscountTest()
        {
            // Arrange
            IOrderCalculator orderCalculator = new HappyHoursOrderCalculator(
                0.1m,
                TimeSpan.Parse("12:30"),
                TimeSpan.Parse("14:00")
                );

            Order order = new Order("ZAM 001/2018", new Customer("M", "S"));
            order.CreateDate = DateTime.Parse("2018-06-19 12:30");

            Product product1 = new Product("Monitor", 1000);
            order.Details.Add(new OrderDetail(product1, 10));

            // Acts
            bool canDiscount = orderCalculator.CanDiscount(order);

            decimal discount = orderCalculator.CalculateDiscount(order);

            

            // 12:30..14 10%

            // Assets
            Assert.AreEqual(1000, discount);
            Assert.IsTrue(canDiscount);

        }
    }
}
