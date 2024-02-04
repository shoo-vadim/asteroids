namespace App.Code.Settings
{
    public class GameSettings
    {
        public float ElementRadius { get; }
        
        public Range<float> AsteroidsSpeed { get; }
        
        public ShipSettings Spaceship { get; }

        public GameSettings(float elementRadius, Range<float> asteroidsSpeed, ShipSettings spaceshipSettings)
        {
            ElementRadius = elementRadius;
            AsteroidsSpeed = asteroidsSpeed;
            Spaceship = spaceshipSettings;
        }
    }
}