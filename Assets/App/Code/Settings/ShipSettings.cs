namespace App.Code.Settings
{
    public class ShipSettings
    {
        public float Rotation { get; }
        
        public float Thrust { get; }
        
        public BulletSettings Bullet { get; }
        
        public LaserSettings Laser { get; }

        public ShipSettings(float rotation, float thrust, BulletSettings bullet, LaserSettings laser)
        {
            Rotation = rotation;
            Thrust = thrust;
            Bullet = bullet;
            Laser = laser;
        }
    }
}