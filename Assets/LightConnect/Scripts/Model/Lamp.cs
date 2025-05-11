namespace LightConnect.Model
{
    public class Lamp : Element
    {
        public Lamp(Colors color) : base(color) { }

        public override ElementTypes Type => ElementTypes.LAMP;
    }
}