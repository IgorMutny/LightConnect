namespace LightConnect.Model
{
    public interface IColoredTile
    {
        Color Color { get; }
        bool ElementPowered { get; }

        void SetElementColor(Color color);
    }
}