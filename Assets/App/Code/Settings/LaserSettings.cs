namespace App.Code.Settings
{
    public class LaserSettings
    {
        public int Amount { get; }
        
        public float Reload { get; }

        public LaserSettings(int amount, float reload)
        {
            Amount = amount;
            Reload = reload;
        }
    }
}