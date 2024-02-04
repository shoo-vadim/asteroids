namespace App.Code.Settings
{
    public class AsteroidsSettings
    {
        public Range<float> Speed { get; }
        
        public Range<float> Spawn { get; }

        public AsteroidsSettings(Range<float> speed, Range<float> spawn)
        {
            Speed = speed;
            Spawn = spawn;
        }
    }
}