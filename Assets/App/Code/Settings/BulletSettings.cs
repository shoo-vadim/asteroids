namespace App.Code.Settings
{
    public class BulletSettings
    {
        public float Lifetime { get; }
        
        public float Reload { get; }
        
        public float Speed { get; }

        public BulletSettings(float lifetime, float reload, float speed)
        {
            Lifetime = lifetime;
            Reload = reload;
            Speed = speed;
        }
    }
}