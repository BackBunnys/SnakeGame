
using SFML.Graphics;
using SFML.Window;
using SnakeGame.Core;
using System.Collections.Generic;

namespace SnakeGame.Settings
{
    class PlayerSettings
    {
        public string Name { get; set; }
        public Color Color { get; set; }
        public Dictionary<MoveDirection, Keyboard.Key> Bindings { get; set; } = new Dictionary<MoveDirection, Keyboard.Key>();
    }
}
