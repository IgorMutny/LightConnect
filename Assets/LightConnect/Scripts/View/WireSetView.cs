using LightConnect.Model;
using UnityEngine;

namespace LightConnect.Core
{
    public class WireSetView : MonoBehaviour
    {
        [SerializeField] private WireSetCenterView _center;
        [SerializeField] private WireView[] _wires;

        private Direction _orientation;

        public void Initialize(TileViewSettings settings)
        {
            _center.Initialize(settings);

            foreach (var wire in _wires)
                wire.Initialize(settings);
        }

        public void SetWireSet(WireSetTypes type)
        {
            if (type != WireSetTypes.NONE)
            {
                _center.gameObject.SetActive(true);
                _center.SetSprite(type);
            }
            else
            {
                _center.gameObject.SetActive(false);
            }

            var directions = WireSetDictionary.GetDirections(type);

            for (int i = 0; i < _wires.Length; i++)
            {
                bool hasWire = directions.Contains((Direction)i);
                _wires[i].SetActive(hasWire);
            }
        }

        public void SetOrientation(Direction orientation)
        {
            _orientation = orientation;
            var rotation = Quaternion.Euler(0, 0, -(int)orientation * 90);
            transform.rotation = rotation;
        }

        public void SetColor(Direction direction, Model.Color color, int order)
        {
            direction -= _orientation;
            _wires[(int)direction].SetColor(color, order);
            _center.SetColor(color, order);
        }

        public void SetLocked(bool value)
        {
            foreach (var wire in _wires)
                wire.SetLocked(value);
        }

        public void CancelColor()
        {
            _center.CancelColor();

            foreach (var wire in _wires)
                wire.CancelColor();
        }
    }
}