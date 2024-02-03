using App.Code.Model.Logical.Field;

namespace App.Code.Tools
{
    public class GameSettings
    {
        public float ElementRadius { get; }
        
        public Range<float> AsteroidsSpeed { get; }

        public GameSettings(float elementRadius, Range<float> asteroidsSpeed)
        {
            ElementRadius = elementRadius;
            AsteroidsSpeed = asteroidsSpeed;
        }
    }
}