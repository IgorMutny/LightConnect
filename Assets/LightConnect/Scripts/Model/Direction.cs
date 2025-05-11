namespace LightConnect.Model
{
    public class Direction
    {
        public const int SIDES_COUNT = 4;

        private Sides _side;

        public Direction(Sides initialDirection)
        {
            _side = initialDirection;
        }

        public Sides Side => _side;

        public void RotateRight()
        {
            _side = Add(_side, Sides.RIGHT);
        }

        public void RotateLeft()
        {
            _side = Add(_side, Sides.LEFT);
        }

        public static Sides Add(Sides side1, Sides side2)
        {
            int a = (int)side1;
            int b = (int)side2;

            int newSide = a + b;
            if (newSide >= SIDES_COUNT)
                newSide -= SIDES_COUNT;

            return (Sides)newSide;
        }
    }
}