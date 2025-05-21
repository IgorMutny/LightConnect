namespace LightConnect.Model
{
    public interface IColoredTile
    {
        Color Color { get; }
        bool Powered { get; }

        void SetColor(Color color);
    }
}