using R3;

namespace LightConnect.Model
{
    public class Element
    {
        private ReactiveProperty<ElementTypes> _type = new();
        private ReactiveProperty<Colors> _color = new();

        public Element()
        {
            _type.Value = ElementTypes.NONE;
            _color.Value = Colors.NONE;
        }

        public ReadOnlyReactiveProperty<ElementTypes> Type => _type;
        public ReadOnlyReactiveProperty<Colors> Color => _color;

        public void SetType(ElementTypes type)
        {
            _type.Value = type;
        }

        public void SetColor(Colors color)
        {
            _color.Value = color;
        }
    }
}