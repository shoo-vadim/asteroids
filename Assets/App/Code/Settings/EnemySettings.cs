using System;
using UnityEngine;

namespace App.Code.Settings
{
    [Serializable]
    public class EnemySettings
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float Radius { get; private set; }
        [field: SerializeField] public Range<float> Spawn { get; private set; }
        [field: SerializeField] public BulletSettings Bullet { get; private set; }
    }
}