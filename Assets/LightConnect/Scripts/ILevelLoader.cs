using LightConnect.Model;

namespace LightConnect
{
    public interface ILevelLoader
    {
        public LevelData Load(int number);
    }
}