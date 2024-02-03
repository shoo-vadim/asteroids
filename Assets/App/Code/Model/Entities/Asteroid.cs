using App.Code.Model.Entities.Base;
using UnityEngine;

namespace App.Code.Model.Entities
{
    public class Asteroid : Body
    {
        public bool IsFragment { get; }
        
        protected Asteroid(Vector2 position, Vector2 movement, float radius, bool isFragment) : 
            base(position, movement, radius)
        {
            IsFragment = isFragment;
        }
    }
}