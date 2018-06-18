namespace Altkom.BPSC.CSharp.Shop.Models
{
    public class Service : Item
    {
        public Service(string name, decimal unitPrice, float ratio)
           : base(name, unitPrice)
        {
            this.Ratio = ratio;
        }

        public float Ratio { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()} {Ratio}";
        }
    }
    

}
