using Cysharp.Threading.Tasks;

namespace LightConnect.Infrastructure
{
    public interface ILevelLoader
    {
        public UniTask<int[]> Load(int levelId);
    }
}