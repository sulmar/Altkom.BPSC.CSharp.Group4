using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altkom.BPSC.CSharp.Shop.Models
{
    public class Order : Base
    {
        // back field
        private string orderNumber;

        //public void SetOrderNumber(string number)
        //{
        //    orderNumber = number;
        //}

        //public string GetOrderNumber()
        //{
        //    return orderNumber;
        //}

        public string OrderNumber
        {
            set
            {
                orderNumber = value;
            }
            get
            {
                return orderNumber;
            }
        }

        // Właściwość (property)
        public DateTime CreateDate { get; set; }
        public DateTime? DeliveryDate { get; set; }

        public Customer Customer { get; set; }

        public List<OrderDetail> Details { get; set; }

        public Order()
        {
            Details = new List<OrderDetail>();

            CreateDate = DateTime.Now;
        }

        // Konstruktor bezparametryczny
        public Order(string orderNumber, Customer customer)
            : this()
        {
            this.OrderNumber = orderNumber;
            this.Customer = customer;
        }

        public Order(string orderNumber)
            : this(orderNumber, new Customer("Marcin", "Sulecki"))
        {
            
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{OrderNumber} {CreateDate}");
            sb.AppendLine("---------------------------");

            foreach (OrderDetail detail in this.Details)
            {
                sb.AppendLine(detail.ToString());
            }

            return sb.ToString();
        }



    }
}
