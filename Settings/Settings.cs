using SFML.System;
using static SnakeGame.Screen.GameSetup;

namespace SnakeGame.Settings
{
    class Settings
    {
        public PlayerSettings Player1 { get; set; } = new PlayerSettings();
        public PlayerSettings Player2 { get; set; } = new PlayerSettings();
        public GameDifficulty Difficulty { get; set; }
        public Vector2u FieldSize { get; set; }
    }
}
