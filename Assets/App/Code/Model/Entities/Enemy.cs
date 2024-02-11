using App.Code.Model.Entities.Base;
using UnityEngine;

namespace App.Code.Model.Entities
{
    public class Enemy : Body
    {
        protected Enemy(Vector2 position, Vector2 movement, float radius) : base(position, movement, radius)
        {
        }
    }
}