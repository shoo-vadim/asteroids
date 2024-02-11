using App.Code.Model.Entities.Base;
using UnityEngine;

namespace App.Code.Model.Entities
{
    public class Bullet : Element
    {
        private float _lifetime;

        public Bullet(Vector2 position, Vector2 movement, float lifetime) : 
            base(position, movement)
        {
            _lifetime = lifetime;
        }

        public bool ApplyLifetime(float deltaTime)
        {
            return (_lifetime -= deltaTime) > 0;
        }
    }
}