using static SnakeGame.Core.Controller.SnakeBotController;
using static SnakeGame.Screen.GameSetup;

namespace SnakeGame.Utils
{
    static class GameSetupExtensions
    {
        public static float GetBlockFactor(this GameDifficulty difficulty)
        {
            switch (difficulty)
            {
                case GameDifficulty.EASY: return 0;
                case GameDifficulty.NORMAL: return 0.01f;
                case GameDifficulty.HARD: return 0.05f;
                default: return 0;
            }
        }

        public static float GetSpeedFactor(this GameDifficulty difficulty)
        {
            switch (difficulty)
            {
                case GameDifficulty.EASY: return 0.5f;
                case GameDifficulty.NORMAL: return 0.75f;
                case GameDifficulty.HARD: return 1f;
                default: return 1f;
            }
        }

        public static Difficulty GetBotDifficulty(this GameDifficulty difficulty)
        {
            switch (difficulty)
            {
                case GameDifficulty.EASY: return Difficulty.EASY;
                case GameDifficulty.NORMAL: return Difficulty.NORMAL;
                case GameDifficulty.HARD: return Difficulty.HARD;
                default: return Difficulty.NORMAL;
            }
        }
    }
}
