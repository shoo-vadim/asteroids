namespace App.Code.Settings
{
    public class GameSettings
    {
        public float ElementRadius { get; }
        
        public Range<float> AsteroidsSpeed { get; }
        
        public ShipSettings Ship { get; }

        public GameSettings(float elementRadius, Range<float> asteroidsSpeed, ShipSettings shipSettings)
        {
            ElementRadius = elementRadius;
            AsteroidsSpeed = asteroidsSpeed;
            Ship = shipSettings;
        }
    }
}