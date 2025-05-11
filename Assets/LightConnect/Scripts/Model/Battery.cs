namespace LightConnect.Model
{
    public class Battery : Element
    {
        public Battery(Colors color) : base(color) { }

        public override ElementTypes Type => ElementTypes.BATTERY;
    }
}