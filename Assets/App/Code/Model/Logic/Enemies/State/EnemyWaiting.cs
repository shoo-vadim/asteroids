using App.Code.Settings;
using UnityEngine;

namespace App.Code.Model.Logic.Enemies.State
{
    public sealed class EnemyWaiting : EnemyState
    {
        private float _left;

        public EnemyWaiting(Range<float> spawn)
        {
            _left = Random.Range(spawn.Min, spawn.Max);
        }

        public bool IsDone(float deltaTime)
        {
            return (_left -= deltaTime) < 0;
        }
    }
}