namespace App.Code.Settings
{
    public class GameSettings
    {
        public float ElementRadius { get; }
        
        public ShipSettings Spaceship { get; }

        public AsteroidsSettings Asteroids { get; }

        public GameSettings(float elementRadius, ShipSettings spaceship, AsteroidsSettings asteroids)
        {
            ElementRadius = elementRadius;
            Spaceship = spaceship;
            Asteroids = asteroids;
        }
    }
}