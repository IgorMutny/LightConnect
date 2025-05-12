using UnityEngine;

namespace LightConnect.LevelConstruction
{
    public abstract class Panel : MonoBehaviour
    {
        protected Constructor Constructor;

        public void Initialize(Constructor constructionService)
        {
            Constructor = constructionService;
            Subscribe();
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }

        protected abstract void Subscribe();
        protected abstract void Unsubscribe();
    }
}