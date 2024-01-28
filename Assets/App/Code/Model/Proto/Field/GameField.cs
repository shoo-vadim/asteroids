using UnityEngine;

namespace App.Code.Model.Proto.Field
{
    public class GameField
    {
        private readonly Range<float> _x;
        private readonly Range<float> _y;

        public GameField(float x, float y) : this(new Range<float>(-x, +x), new Range<float>(-y, +y))
        {
        }

        private GameField(Range<float> x, Range<float> y)
        {
            _x = x;
            _y = y;
        }

        public Vector2 GetRandomPosition()
        {
            return new Vector2(
                Random.Range(_x.Min, _x.Max), 
                Random.Range(_y.Min, _y.Max));
        }

        public MirroredPosition GetMirroredPosition(Vector2 position)
        {
            if (position.x < _x.Min)
                return new MirroredPosition(true, new Vector2(_x.Max, position.y));
            
            if (position.x > _x.Max) 
                return new MirroredPosition(true, new Vector2(_x.Min, position.y));
            
            if (position.y < _y.Min)
                return new MirroredPosition(true, new Vector2(position.x, _y.Max));
            
            if (position.y > _y.Max)
                return new MirroredPosition(true, new Vector2(position.x, _y.Min));
            
            return new MirroredPosition(false, Vector2.zero);
        }
    }
}