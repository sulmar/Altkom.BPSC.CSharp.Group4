using Altkom.BPSC.CSharp.Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altkom.BPSC.CSharp.Shop.ConsoleClient
{
   

    class Program
    {
        static void Main(string[] args)
        {
            DateTimeHelper.IsHoliday(DateTime.Today);

            DateTime.Today.IsHoliday();

            Printer printer = new Printer();

            printer.Print(DateTime.Now);

            CreateOrderTest();
        }

      
        private static void CreateOrderTest()
        {
            Order order;

            Customer customer = new Customer("Marcin", "Sulecki");

            order = new Order("ZAM 001/2018", customer);

            Product product1 = new Product("Monitor", 15);
            product1.UnitPrice = 0;

            string input = "S";

           // Item item = ItemFactory.Create(input);


            Service service1 = new Service("Usługa programistyczna", 100, 1);

            OrderDetail orderDetail1 = new OrderDetail(product1, 5);
            OrderDetail orderDetail2 = new OrderDetail(service1, 1);

            order.Details.Add(orderDetail1);
            order.Details.Add(orderDetail2);


            Console.WriteLine($"{order.ToString()}");
          



        }
    }
}
