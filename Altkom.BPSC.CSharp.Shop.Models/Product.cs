namespace Altkom.BPSC.CSharp.Shop.Models
{
    public class Product : Item
    {
        public Product(string name, decimal unitPrice)
            : base(name, unitPrice)
        {
            this.Color = "Black";
        }

        public string Color { get; set; }
        public short Stock { get; set; }

        public override string ToString()
        {
           

            return $"{base.ToString()} {Color}";
        }
    }
    

}
