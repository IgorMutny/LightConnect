using R3;

namespace LightConnect.Core
{
    public class Direction
    {
        public const int DIRECTIONS_COUNT = 4;
        
        private readonly ReactiveProperty<Directions> _value = new();

        public Direction(Directions initialDirection)
        {
            _value.Value = initialDirection;
        }

        public ReadOnlyReactiveProperty<Directions> Value => _value;

        public void RotateClockwise()
        {
            int newDirection = (int)_value.Value;

            newDirection += 1;
            if (newDirection == DIRECTIONS_COUNT)
                newDirection = 0;

            _value.Value = (Directions)newDirection;
        }
    }
}