using System;
using UnityEngine;

namespace App.Code.Settings
{
    [Serializable]
    public class BulletSettings
    {
        [field: SerializeField] public float Lifetime { get; private set; }
        [field: SerializeField] public float Reload { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
    }
}