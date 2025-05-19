using Cysharp.Threading.Tasks;
using LightConnect.Model;

namespace LightConnect.Infrastructure
{
    public interface ILevelLoader
    {
        public UniTask<LevelData> Load(int levelId);
    }
}