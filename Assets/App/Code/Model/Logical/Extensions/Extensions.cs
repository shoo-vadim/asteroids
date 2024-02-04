using App.Code.Model.Entities;
using UnityEngine;

namespace App.Code.Model.Logical.Extensions
{
    public static class Extensions
    {
        public static Asteroid CreateFragment(this Asteroid asteroid, float movementModifier)
        {
            return new Asteroid(
                asteroid.Position,
                asteroid.Movement.GetRotated(Random.Range(0, 360) * movementModifier),
                asteroid.Radius,
                true);
        }
    }
}