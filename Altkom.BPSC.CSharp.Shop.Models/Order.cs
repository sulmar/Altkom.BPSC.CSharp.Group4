using Stateless;
using Stateless.Graph;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altkom.BPSC.CSharp.Shop.Models
{
    public enum OrderStatus
    {
        Created,
        Completing,
        Sent,
        Delivered,
        Canceled
    }

    public enum OrderTrigger
    {
        Complete,
        Send,
        Cancel
    }

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

        // public OrderStatus Status { get; set; }

        public List<OrderDetail> Details { get; set; }


        //public decimal TotalAmount
        //{
        //    get
        //    {
        //        decimal totalAmount = 0;

        //        foreach (OrderDetail detail in Details)
        //        {
        //            totalAmount += detail.Amount;
        //        }

        //        return totalAmount;
        //    }

        //}

        public decimal TotalAmount
        {
            get
            {
                return Details.Sum(detail => detail.Amount);
            }
        }

        private StateMachine<OrderStatus, OrderTrigger> machine;

        public OrderStatus Status
        {
            get
            {
                return machine.State;
            }
        }
           

        public Order()
        {
            Details = new List<OrderDetail>();

            CreateDate = DateTime.Now;
            // Status = OrderStatus.Created;

            machine = new StateMachine<OrderStatus, OrderTrigger>(OrderStatus.Created);

            machine.Configure(OrderStatus.Created)
                .Permit(OrderTrigger.Complete, OrderStatus.Completing)
                .OnEntry(() => Console.WriteLine("Trwa kompletacja"));

            machine.Configure(OrderStatus.Completing)
                .Permit(OrderTrigger.Send, OrderStatus.Sent)
                .Permit(OrderTrigger.Cancel, OrderStatus.Canceled)
                ;

            string graph = UmlDotGraph.Format(machine.GetInfo());

            Trace.WriteLine(graph);
        }

        public void Complete()
        {
            machine.Fire(OrderTrigger.Complete);
        }

        public void SentBox()
        {
            machine.Fire(OrderTrigger.Send);
        }

        public void Cancel()
        {
            machine.Fire(OrderTrigger.Cancel);
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
