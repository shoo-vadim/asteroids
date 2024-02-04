using App.Code.Model.Binding.Interfaces.Custom;
using App.Code.Model.Entities.Base;
using UnityEngine;

namespace App.Code.Model.Entities
{
    public class Bullet : Element, IBullet
    {
        public float RemainingLifetime { get; private set; }

        public Bullet(Vector2 position, Vector2 movement, float lifetime) : 
            base(position, movement)
        {
            RemainingLifetime = lifetime;
        }

        public bool ApplyLifetime(float deltaTime)
        {
            RemainingLifetime -= deltaTime;
            return RemainingLifetime > 0;
        }
    }
}