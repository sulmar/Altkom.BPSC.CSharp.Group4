using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altkom.BPSC.CSharp.Shop.Models
{
    public class OrderDetail : Base
    {
        public OrderDetail(Item item, int quantity = 1)
        {
            Item = item;
            Quantity = quantity;

            UnitPrice = item.UnitPrice;
        }

        public Item Item { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public decimal Amount
        {
            get
            {
                return Quantity * UnitPrice;
            }
        }

        public override string ToString()
        {
            return $"{Item} {Quantity} {UnitPrice:C2}";


            // zła praktyka

            //if (Item is Product)
            //{
            //    Product product = (Product)Item;

            //    return $"{Item} {product.Color} {Quantity} {UnitPrice:C2}";
            //}
            //else
            //if (Item is Service)
            //{
            //    Service service = (Service)Item;

            //    return $"{Item.Name} {service.Ratio} {Quantity} {UnitPrice:C2}";
            //}
            //else
            //return $"{Item.Name} {Quantity} {UnitPrice:C2}";
        }
    }
}
