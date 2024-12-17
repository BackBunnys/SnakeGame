using SFML.Graphics;
using SFML.Window;
using SnakeGame.Core;
using System;
using static SnakeGame.Screen.GameSetup;

namespace SnakeGame.Settings
{
    class AppConfigSettingsManager : ISettingsManager
    {
        public Settings Get()
        {
            return MapToSettings(Properties.Settings.Default);
        }

        public void Save(Settings settings)
        {
            Merge(Properties.Settings.Default, settings);
            Properties.Settings.Default.Save();
        }

        private Settings MapToSettings(Properties.Settings properties)
        {
            Settings settings = new Settings();

            settings.Difficulty = (GameDifficulty)Enum.Parse(typeof(GameDifficulty), properties.difficulty);

            settings.Player1.Name = properties.player1_name;
            settings.Player1.Color = new Color(properties.player1_color.R, properties.player1_color.G, properties.player1_color.B);
            settings.Player1.Bindings[MoveDirection.UP] = (Keyboard.Key)Enum.Parse(typeof(Keyboard.Key), properties.player1_up);
            settings.Player1.Bindings[MoveDirection.DOWN] = (Keyboard.Key)Enum.Parse(typeof(Keyboard.Key), properties.player1_down);
            settings.Player1.Bindings[MoveDirection.LEFT] = (Keyboard.Key)Enum.Parse(typeof(Keyboard.Key), properties.player1_left);
            settings.Player1.Bindings[MoveDirection.RIGHT] = (Keyboard.Key)Enum.Parse(typeof(Keyboard.Key), properties.player1_right);

            settings.Player2.Name = properties.player2_name;
            settings.Player2.Color = new Color(properties.player2_color.R, properties.player2_color.G, properties.player2_color.B);
            settings.Player2.Bindings[MoveDirection.UP] = (Keyboard.Key)Enum.Parse(typeof(Keyboard.Key), properties.player2_up);
            settings.Player2.Bindings[MoveDirection.DOWN] = (Keyboard.Key)Enum.Parse(typeof(Keyboard.Key), properties.player2_down);
            settings.Player2.Bindings[MoveDirection.LEFT] = (Keyboard.Key)Enum.Parse(typeof(Keyboard.Key), properties.player2_left);
            settings.Player2.Bindings[MoveDirection.RIGHT] = (Keyboard.Key)Enum.Parse(typeof(Keyboard.Key), properties.player2_right);

            return settings;
        }

        private void Merge(Properties.Settings properties, Settings settings)
        {
            properties.difficulty = settings.Difficulty.ToString();
            properties.player1_name = settings.Player1.Name;
            properties.player1_color = System.Drawing.Color.FromArgb(settings.Player1.Color.R, settings.Player1.Color.G, settings.Player1.Color.B);
            properties.player1_up = settings.Player1.Bindings[MoveDirection.UP].ToString();
            properties.player1_down = settings.Player1.Bindings[MoveDirection.DOWN].ToString();
            properties.player1_left = settings.Player1.Bindings[MoveDirection.LEFT].ToString();
            properties.player1_right = settings.Player1.Bindings[MoveDirection.RIGHT].ToString();

            properties.player2_name = settings.Player2.Name;
            properties.player2_color = System.Drawing.Color.FromArgb(settings.Player2.Color.R, settings.Player2.Color.G, settings.Player2.Color.B);
            properties.player2_up = settings.Player2.Bindings[MoveDirection.UP].ToString();
            properties.player2_down = settings.Player2.Bindings[MoveDirection.DOWN].ToString();
            properties.player2_left = settings.Player2.Bindings[MoveDirection.LEFT].ToString();
            properties.player2_right = settings.Player2.Bindings[MoveDirection.RIGHT].ToString();
        }
    }
}
