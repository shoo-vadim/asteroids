using App.Code.Model.Binding;
using App.Code.View;
using UnityEngine;

namespace App.Code.Model.Proto.Data
{
    public class EntityLegacy
    {
        public ElementType ElementType;
        
        public Vector2 Movement { get; set; }

        public Vector2 Position { get; set; }
        public float Radius { get; set; }
        
        public void ApplyMovement(float deltaTime)
        {
            Position += Movement * deltaTime;
        }
    }
}