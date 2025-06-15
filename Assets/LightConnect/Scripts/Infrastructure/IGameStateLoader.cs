namespace LightConnect.Infrastructure
{
    public interface IGameStateLoader
    {
        GameData Load();
        void Save(GameData data);
    }
}