using App.Code.Model.Binding.Interfaces.Custom;
using App.Code.Model.Entities.Base;
using UnityEngine;

namespace App.Code.Model.Entities
{
    public class Asteroid : Body, IAsteroid
    {
        public bool IsFragment { get; }
        
        public Asteroid(Vector2 position, Vector2 movement, float radius, bool isFragment) : 
            base(position, movement, radius)
        {
            IsFragment = isFragment;
        }
    }
}