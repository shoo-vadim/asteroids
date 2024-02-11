using System;
using UnityEngine;

namespace App.Code.Settings
{
    [Serializable]
    public class AsteroidSettings
    {
        [field: SerializeField] public Range<float> Speed { get; private set; }
        [field: SerializeField] public Range<float> Spawn { get; private set; }
    }
}