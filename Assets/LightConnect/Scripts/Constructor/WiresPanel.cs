using LightConnect.Core;
using LightConnect.Model;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace LightConnect.Constructor
{
    public class WiresPanel : Panel
    {
        [SerializeField] private Button _none;
        [SerializeField] private Button _single;
        [SerializeField] private Button _straight;
        [SerializeField] private Button _bent;
        [SerializeField] private Button _triple;

        protected override void Subscribe()
        {
            _none.onClick.AddListener(SelectNone);
            _single.onClick.AddListener(SelectSingle);
            _straight.onClick.AddListener(SelectStraight);
            _bent.onClick.AddListener(SelectBent);
            _triple.onClick.AddListener(SelectTriple);
        }

        protected override void Unsubscribe()
        {
            _none.onClick.RemoveListener(SelectNone);
            _single.onClick.RemoveListener(SelectSingle);
            _straight.onClick.RemoveListener(SelectStraight);
            _bent.onClick.RemoveListener(SelectBent);
            _triple.onClick.RemoveListener(SelectTriple);
        }

        private void SelectWire(WireTypes type)
        {
            LevelPresenter.SetWire(type);
        }

        private void SelectSingle()
        {
            SelectWire(WireTypes.SINGLE);
        }

        private void SelectStraight()
        {
            SelectWire(WireTypes.STRAIGHT);
        }

        private void SelectBent()
        {
            SelectWire(WireTypes.BENT);
        }

        private void SelectTriple()
        {
            SelectWire(WireTypes.TRIPLE);
        }

        private void SelectNone()
        {
            SelectWire(WireTypes.NONE);
        }
    }
}