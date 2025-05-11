namespace LightConnect.Model
{
    public abstract class Element
    {
        public readonly Colors Color;

        public Element(Colors color)
        {
            Color = color;
        }

        public abstract ElementTypes Type { get; }
    }
}