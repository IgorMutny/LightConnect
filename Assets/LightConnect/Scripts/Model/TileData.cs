using System;
using UnityEngine;

namespace LightConnect.Model
{
    [Serializable]
    public class TileData
    {
        private const int TYPE_OFFSET = 28;
        private const int POSITION_X_OFFSET = 24;
        private const int POSITION_Y_OFFSET = 20;
        private const int WIRESET_TYPE_OFFSET = 16;
        private const int ORIENTATION_OFFSET = 12;
        private const int ADDITIONAL_1_OFFSET = 8;
        private const int ADDITIONAL_2_OFFSET = 4;

        [SerializeField] private int _value;

        public TileTypes Type
        {
            get
            {
                int type = GetBytes(TYPE_OFFSET);
                return (TileTypes)type;
            }

            set
            {
                int bytes = (int)value;
                SetBytes(bytes, TYPE_OFFSET);
            }
        }

        public Vector2Int Position
        {
            get
            {
                int x = GetBytes(POSITION_X_OFFSET);
                int y = GetBytes(POSITION_Y_OFFSET);
                return new Vector2Int(x, y);
            }

            set
            {
                int x = value.x;
                SetBytes(x, POSITION_X_OFFSET);
                int y = value.y;
                SetBytes(y, POSITION_Y_OFFSET);
            }
        }

        public WireSetTypes WireSetType
        {
            get
            {
                int type = GetBytes(WIRESET_TYPE_OFFSET);
                return (WireSetTypes)type;
            }

            set
            {
                int bytes = (int)value;
                SetBytes(bytes, WIRESET_TYPE_OFFSET);
            }
        }

        public Direction Orientation
        {
            get
            {
                int orientation = GetBytes(ORIENTATION_OFFSET);
                return (Direction)orientation;
            }

            set
            {
                int bytes = (int)value;
                SetBytes(bytes, ORIENTATION_OFFSET);
            }
        }

        public Color Color
        {
            get
            {
                int color = GetBytes(ADDITIONAL_1_OFFSET);
                return (Color)color;
            }

            set
            {
                int bytes = (int)value;
                SetBytes(bytes, ADDITIONAL_1_OFFSET);
            }
        }

        public Vector2Int ConnectedPosition
        {
            get
            {
                int x = GetBytes(ADDITIONAL_1_OFFSET);
                int y = GetBytes(ADDITIONAL_2_OFFSET);
                return new Vector2Int(x, y);
            }

            set
            {
                int x = value.x;
                SetBytes(x, ADDITIONAL_1_OFFSET);
                int y = value.y;
                SetBytes(y, ADDITIONAL_2_OFFSET);
            }
        }

        private int GetBytes(int offset)
        {
            return (_value & (0b1111 << offset)) >> offset;
        }

        private void SetBytes(int bytes, int offset)
        {
            ClearBytes(offset);
            _value |= bytes << offset;
        }

        private void ClearBytes(int offset)
        {
            int mask = 0;
            mask = ~mask;
            mask ^= 0b1111 << offset;
            _value &= mask;
        }
        public override string ToString()
        {
            return Convert.ToString(_value, 2).PadLeft(32, '0');
        }

        public static explicit operator int(TileData data)
        {
            return data._value;
        }

        public static explicit operator TileData(int value)
        {
            return new TileData() { _value = value };
        }
    }
}