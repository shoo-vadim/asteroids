using App.Code.Model.Proto.Entities.Base;
using UnityEngine;


namespace App.Code.Model.Proto.Entities
{
    public class Bullet : Entity
    {
        public float RemainingLifetime { get; set; }

        protected Bullet(Vector2 position, Vector2 movement, float lifetime) : 
            base(position, movement)
        {
            RemainingLifetime = lifetime;
        }
    }
}