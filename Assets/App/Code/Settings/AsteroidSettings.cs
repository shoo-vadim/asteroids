namespace App.Code.Settings
{
    public class AsteroidSettings
    {
        public Range<float> Speed { get; }
        
        public Range<float> Spawn { get; }

        public AsteroidSettings(Range<float> speed, Range<float> spawn)
        {
            Speed = speed;
            Spawn = spawn;
        }
    }
}