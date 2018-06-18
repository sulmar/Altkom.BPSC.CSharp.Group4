namespace Altkom.BPSC.CSharp.Shop.Models
{
    public abstract class Item : Base
    {
        protected Item(string name, decimal unitPrice)
        {
            Name = name;
            UnitPrice = unitPrice;
        }

        public string Name { get; set; }
        public decimal UnitPrice { get; set; }


        public override string ToString()
        {
            return $"{Name} {UnitPrice:C2}";
        }
    }
    

}
