using System;
using LightConnect.Model;
using UnityEngine;
using UnityEngine.UI;

namespace LightConnect.Construction
{
    public class WiresPanel : MonoBehaviour
    {
        [SerializeField] private Button _none;
        [SerializeField] private Button _single;
        [SerializeField] private Button _straight;
        [SerializeField] private Button _bent;
        [SerializeField] private Button _triple;

        public event Action<WireSetTypes> SetWireSetTypeRequired;

        private void Start()
        {
            _none.onClick.AddListener(SelectNone);
            _single.onClick.AddListener(SelectSingle);
            _straight.onClick.AddListener(SelectStraight);
            _bent.onClick.AddListener(SelectBent);
            _triple.onClick.AddListener(SelectTriple);
        }

        private void OnDestroy()
        {
            _none.onClick.RemoveListener(SelectNone);
            _single.onClick.RemoveListener(SelectSingle);
            _straight.onClick.RemoveListener(SelectStraight);
            _bent.onClick.RemoveListener(SelectBent);
            _triple.onClick.RemoveListener(SelectTriple);
        }

        private void SelectWire(WireSetTypes type)
        {
            SetWireSetTypeRequired?.Invoke(type);
        }

        private void SelectSingle()
        {
            SelectWire(WireSetTypes.SINGLE);
        }

        private void SelectStraight()
        {
            SelectWire(WireSetTypes.STRAIGHT);
        }

        private void SelectBent()
        {
            SelectWire(WireSetTypes.BENT);
        }

        private void SelectTriple()
        {
            SelectWire(WireSetTypes.TRIPLE);
        }

        private void SelectNone()
        {
            SelectWire(WireSetTypes.NONE);
        }
    }
}