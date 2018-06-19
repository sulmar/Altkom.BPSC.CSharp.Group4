using Altkom.BPSC.CSharp.Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altkom.BPSC.CSharp.Shop.OrderCalculators
{

    public interface ICanDiscountStrategy
    {
        bool CanDiscount(Order order);
    }

    public interface IDiscountStrategy
    {
        decimal CalculateDiscount(Order order);
    }

    public class HappyHoursCanDiscountStrategy : ICanDiscountStrategy
    {
        private readonly TimeSpan beginTime;
        private readonly TimeSpan endTime;

        public HappyHoursCanDiscountStrategy(
            TimeSpan beginTime, 
            TimeSpan endTime)
        {
            this.beginTime = beginTime;
            this.endTime = endTime;
        }

        public bool CanDiscount(Order order)
        {
            return order.CreateDate.TimeOfDay >= beginTime
               && order.CreateDate.TimeOfDay <= endTime;
        }
    }

    public class PercentageDiscountStrategy : IDiscountStrategy
    {
        private readonly decimal percentage;

        public PercentageDiscountStrategy(decimal percentage)
        {
            this.percentage = percentage;
        }

        public decimal CalculateDiscount(Order order)
        {
            return order.TotalAmount * percentage;
        }
    }

    public class FixedDiscountStrategy : IDiscountStrategy
    {
        private readonly decimal discount;

        public FixedDiscountStrategy(decimal discount)
        {
            this.discount = discount;
        }

        public decimal CalculateDiscount(Order order)
        {
            const decimal minAmount = 1;

            if (order.TotalAmount - minAmount > discount)
                return discount;
            else
                return order.TotalAmount - minAmount;
        }
    }

    public class OrderCalculator
    {
        private readonly ICanDiscountStrategy canDiscountStrategy;
        private readonly IDiscountStrategy discountStrategy;

        public OrderCalculator(
            ICanDiscountStrategy canDiscountStrategy, 
            IDiscountStrategy discountStrategy)
        {
            this.canDiscountStrategy = canDiscountStrategy;
            this.discountStrategy = discountStrategy;
        }

        public decimal CalculateDiscount(Order order)
        {
            if (canDiscountStrategy.CanDiscount(order))
            {
                return discountStrategy.CalculateDiscount(order);
            }
            else
            {
                return 0;
            }

        }
    }
}
