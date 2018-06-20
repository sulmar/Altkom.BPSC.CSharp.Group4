using Altkom.BPSC.CSharp.Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altkom.BPSC.CSharp.Shop.Printers
{
    class Printer
    {
        public delegate void Log(string message);

        public Log log;

        public delegate void FinishedHandler(string message);

        public event FinishedHandler Finished;

        //public delegate void PrintingHandler(object sender, EventArgs e);
        //public event PrintingHandler Printing;

        public event EventHandler<EventArgs> Printing;


        public Printer()
        {
            log += Console.WriteLine;

            Finished += Console.WriteLine;
        }

        public void Print<TEntity>(TEntity entity)
        {
            Printing?.Invoke(this, new EventArgs());

            Console.WriteLine(entity);

            // TODO zapisz do logu

            //if (log != null)
            //{
            //    log.Invoke($"LOG: {entity}");
            //}

            // C# 6.0
            log?.Invoke($"LOG: {entity}");

            Finished?.Invoke("koniec wydruku");
        }




    }
}
