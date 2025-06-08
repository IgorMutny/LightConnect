using System;
using System.Collections.Generic;
using LightConnect.View;
using UnityEngine;

namespace LightConnect.Construction
{
    public class ConstructorView : MonoBehaviour
    {
        [field: SerializeField] public LevelView Level { get; private set; }
        [field: SerializeField] public Panels Panels { get; private set; }
        [field: SerializeField] public Placeholders Placeholders { get; private set; }
    }
}