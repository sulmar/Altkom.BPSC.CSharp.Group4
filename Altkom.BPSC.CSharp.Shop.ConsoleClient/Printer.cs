using Altkom.BPSC.CSharp.Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altkom.BPSC.CSharp.Shop.ConsoleClient
{
    class Printer
    {
        public void Print<TEntity>(TEntity entity)
            where TEntity : struct
        {
            Console.WriteLine(entity);
        }


    }
}
