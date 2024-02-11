using System;
using UnityEngine;

namespace App.Code.Settings
{
    [Serializable]
    public class LaserSettings
    {
        [field: SerializeField] public int Amount { get; private set; }
        
        [field: SerializeField] public float Reload { get; private set; }
    }
}