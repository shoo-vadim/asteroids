using System;
using UnityEngine;

namespace App.Code.Settings
{
    [Serializable]
    public class ShipSettings
    {
        [field: SerializeField] public float Rotation { get; private set; }
        
        [field: SerializeField] public float Thrust { get; private set; }
        
        [field: SerializeField] public BulletSettings Bullet { get; private set; }
        
        [field: SerializeField]  public LaserSettings Laser { get; private set; }
    }
}