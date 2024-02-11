using App.Code.Model.Entities.Base;
using App.Code.Model.Logical.Field;
using UnityEngine;

namespace App.Code.Model.Entities
{
    public class Enemy : Body
    {
        private readonly float _speed;
        
        public Enemy(Vector2 position, float speed, float radius) : 
            base(position, Vector2.zero, radius)
        {
            _speed = speed;
        }

        public void ApplyMovementTowards(Vector2 position, float deltaTime, GameField field)
        {
            // Movement = 
            ApplyMovement(deltaTime, field);
        }
    }
}