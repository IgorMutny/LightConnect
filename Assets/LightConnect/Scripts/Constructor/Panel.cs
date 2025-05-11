using UnityEngine;

namespace LightConnect.Constructor
{
    public abstract class Panel : MonoBehaviour
    {
        protected LevelPresenter LevelPresenter;

        public void Initialize(LevelPresenter levelPresenter)
        {
            LevelPresenter = levelPresenter;
            Subscribe();
        }

        public void Reinitialize(LevelPresenter levelPresenter)
        {
            LevelPresenter = levelPresenter;
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }

        protected abstract void Subscribe();
        protected abstract void Unsubscribe();
    }
}