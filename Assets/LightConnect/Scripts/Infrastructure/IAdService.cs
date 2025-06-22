using Cysharp.Threading.Tasks;

namespace LightConnect.Infrastructure
{
    public interface IAdService
    {
        UniTask ShowInterstitialsIfAllowed();
        UniTask<bool> ShowRewarded();
    }
}