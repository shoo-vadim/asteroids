namespace App.Code.Settings
{
    public class GameSettings
    {
        public ShipSettings Spaceship { get; }

        public AsteroidSettings Asteroid { get; }

        public GameSettings(ShipSettings spaceship, AsteroidSettings asteroid)
        {
            Spaceship = spaceship;
            Asteroid = asteroid;
        }
    }
}