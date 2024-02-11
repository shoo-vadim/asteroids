using System;
using UnityEngine;

namespace App.Code.Settings
{
    [Serializable]
    public class GameSettings
    {
        [field: SerializeField] public ShipSettings Spaceship { get; private set; }

        [field: SerializeField] public AsteroidSettings Asteroid { get; private set; }
    }
}