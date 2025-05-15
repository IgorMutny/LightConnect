namespace LightConnect.Model
{
    public class Wire
    {
        public Wire(Direction orientation)
        {
            Orientation = orientation;
            Color = Color.None;
        }

        public Direction Orientation { get; private set; }
        public Color Color { get; private set; }

        public void Rotate(Direction side)
        {
            Orientation += side;
            Color = Color.None;
        }

        public void AddColor(Color color)
        {
            Color += color;
        }

        public void ResetColor()
        {
            Color = Color.None;
        }
    }
}