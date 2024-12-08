namespace SnakeGame.Screen
{
    class GameSetup
    {
        public enum GameType
        {
            SINGLEPLAYER,
            MULTIPLAYER
        }

        public uint RoundCount { get; set; } = 1;
        public GameType Type { get; set; } = GameType.SINGLEPLAYER;
        public bool VersusBot { get; set; } = false;
    }
}
