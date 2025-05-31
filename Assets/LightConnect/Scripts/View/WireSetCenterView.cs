using LightConnect.Model;

namespace LightConnect.Core
{
    public class WireSetCenterView : TilePartView
    {
        public void SetSprite(WireSetTypes type)
        {
            var sprite = Settings.WireSetCenterSprite(type);
            Image.sprite = sprite;
        }
    }
}