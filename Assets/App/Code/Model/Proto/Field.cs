using UnityEngine;

namespace App.Code.Model
{
    public class Field
    {
        private readonly (float min, float max) _x;
        private readonly (float min, float max) _y;

        public Field((float min, float max) x, (float min, float max) y)
        {
            _x = x;
            _y = y;
        }

        public Vector2 GetRandomPosition()
        {
            return new Vector2(
                Random.Range(_x.min, _y.min), 
                Random.Range(_x.max, _y.max));
        }

        public bool TryGetMirroredPosition()
        {
            return false;
        }
    }
}