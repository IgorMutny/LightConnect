using UnityEngine;
using UnityEngine.UI;

namespace LightConnect.LevelConstruction
{
    public class SizePanel : Panel
    {
        [SerializeField] private Slider _width;
        [SerializeField] private Slider _height;

        protected override void Subscribe()
        {
            _width.value = Constructor.CurrentSize.x;
            _height.value = Constructor.CurrentSize.y;

            _width.onValueChanged.AddListener(OnWidthChanged);
            _height.onValueChanged.AddListener(OnHeightChanged);
        }

        protected override void Unsubscribe()
        {
            _width.onValueChanged.RemoveListener(OnWidthChanged);
            _height.onValueChanged.RemoveListener(OnHeightChanged);
        }

        private void OnWidthChanged(float value)
        {
            ResizeLevel();
        }

        private void OnHeightChanged(float value)
        {
            ResizeLevel();
        }

        private void ResizeLevel()
        {
            int x = (int)_width.value;
            int y = (int)_height.value;
            var size = new Vector2Int(x,y);
            Constructor.ResizeLevel(size);
        }
    }
}