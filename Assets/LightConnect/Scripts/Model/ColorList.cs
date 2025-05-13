using System.Collections.Generic;
using System.Linq;

namespace LightConnect.Model
{
    public class ColorList
    {
        private List<Colors> _colors = new();

        public ColorList()
        {
            _colors.Add(Colors.NONE);
        }

        public void Add(Colors color)
        {
            if (!_colors.Contains(color))
                _colors.Add(color);

            if (_colors.Count > 1)
                _colors.RemoveAll(appliedColor => appliedColor == Colors.NONE);
        }

        public void Clear()
        {
            _colors.Clear();
            _colors.Add(Colors.NONE);
        }

        public Colors GetCurrentColor()
        {
            if (_colors.Count == 1)
                return _colors.First();
            else
                return Colors.NONE;
        }
    }
}