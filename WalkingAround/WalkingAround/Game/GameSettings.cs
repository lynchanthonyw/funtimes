
namespace WalkingAround.Game
{
    public static class GameSettings
    {
        internal const int SECONDSPERGAMEHOUR = 150;
        internal const int TICKSPERGAMEHOUR = 60;
        internal const int GAMESPEEDMULTIPLIER = 10;
        public static GameSpeed GameSpeed { get; private set; }
        public static int ClockTick { get { return ((SECONDSPERGAMEHOUR * 1000) / TICKSPERGAMEHOUR) / ((int)GameSpeed / GAMESPEEDMULTIPLIER); } }

        static GameSettings()
        {
            GameSpeed = Game.GameSpeed.Ultra;
        }
    }
}