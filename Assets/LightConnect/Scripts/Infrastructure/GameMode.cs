namespace LightConnect.Infrastructure
{
    public static class GameMode
    {
        public static Mode Current { get; set; }

        public enum Mode
        {
            GAMEPLAY = 0,
            CONSTRUCTOR = 1,
        }
    }
}