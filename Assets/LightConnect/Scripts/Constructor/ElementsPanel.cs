using UnityEngine;
using UnityEngine.UI;

namespace LightConnect.Constructor
{
    public class ElementsPanel : MonoBehaviour
    {
        [SerializeField] private Button _battery;
        [SerializeField] private Button _lamp;
        [SerializeField] private Button _straightWire;
        [SerializeField] private Button _bentWire;
        [SerializeField] private Button _tripleWire;

        public void Initialize()
        {

        }

        public void Dispose()
        {

        }
    }
}