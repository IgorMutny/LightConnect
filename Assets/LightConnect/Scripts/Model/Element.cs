namespace LightConnect.Model
{
    public class Element
    {
        public Element()
        {
            Type = ElementTypes.NONE;
            Color = Color.None;
        }

        public ElementTypes Type { get; private set; }
        public Color Color { get; private set; }

        public void SetType(ElementTypes type)
        {
            Type = type;
            Color = Color.None;
        }

        public void SetColor(Color color)
        {
            Color = color;
        }
    }
}