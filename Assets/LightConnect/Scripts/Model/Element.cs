namespace LightConnect.Model
{
    public class Element
    {
        public Element()
        {
            Type = TileTypes.WIRE;
            Color = Color.None;
        }

        public TileTypes Type { get; private set; }
        public Color Color { get; private set; }

        public void SetType(TileTypes type)
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