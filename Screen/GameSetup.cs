using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SnakeGame.Core;
using SnakeGame.Core.Controller;
using System.Collections.Generic;

namespace SnakeGame.Screen
{
    class GameSetup //Класс настроек игры
    {
        public enum GameDifficulty
        {
            EASY,
            NORMAL,
            HARD
        }

        public abstract class PlayerSetup
        {
            public string Name { get; set; }
            public Color Color { get; set; }
        }

        public class HumanPlayerSetup : PlayerSetup
        {

            public static readonly HumanPlayerSetup PLAYER_ONE = new HumanPlayerSetup() { Name = "Player 1", Color = Color.Green, Bindings = DefaultKeyboardBindings.PLAYER_ONE };
            public static readonly HumanPlayerSetup PLAYER_TWO = new HumanPlayerSetup() { Name = "Player 2", Color = new Color(255, 165, 0), Bindings = DefaultKeyboardBindings.PLAYER_TWO };
            public Dictionary<Keyboard.Key, MoveDirection> Bindings { get; set; }

        }

        public class BotPlayerSetup : PlayerSetup
        {
            public static readonly BotPlayerSetup BOT_SETUP = new BotPlayerSetup() { Name = "Bot", Color = Color.Yellow };
        }

        public uint? RoundCount { get; set; } = 1;
        public Vector2u FieldSize { get; set; } = new Vector2u(34, 20);
        public GameDifficulty Difficulty { get; set; } = GameDifficulty.NORMAL;

        public List<PlayerSetup> Players { get; set; } = new List<PlayerSetup>() { HumanPlayerSetup.PLAYER_ONE };
    }
}
