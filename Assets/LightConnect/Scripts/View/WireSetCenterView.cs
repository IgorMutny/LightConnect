using LightConnect.Model;

namespace LightConnect.View
{
    public class WireSetCenterView : TilePartView
    {
        public void SetSprite(WireSetTypes type)
        {
            var sprite = Settings.WireSetCenterSprite(type);
            Renderer.sprite = sprite;
        }
    }
}