using Altkom.BPSC.CSharp.Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altkom.BPSC.CSharp.Shop.OrderCalculators
{
    public interface IOrderCalculator
    {
        decimal CalculateDiscount(Order order);
        bool CanDiscount(Order order);
    }
    

    public class HappyHoursOrderCalculator : IOrderCalculator
    {
        private readonly decimal percentage;
        private readonly TimeSpan beginTime;
        private readonly TimeSpan endTime;

        public HappyHoursOrderCalculator(
            decimal percentage, 
            TimeSpan beginTime, 
            TimeSpan endTime)
        {
            this.percentage = percentage;
            this.beginTime = beginTime;
            this.endTime = endTime;
        }

        public decimal CalculateDiscount(Order order)
        {
            return order.TotalAmount * percentage;
        }

        public bool CanDiscount(Order order)
        {
            return order.CreateDate.TimeOfDay >= beginTime
               && order.CreateDate.TimeOfDay <= endTime;
        }
    }


}
