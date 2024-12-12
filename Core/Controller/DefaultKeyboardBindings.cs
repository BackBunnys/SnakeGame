using SFML.Window;
using System.Collections.Generic;

namespace SnakeGame.Core.Controller
{
    static class DefaultKeyboardBindings
    {
        public static readonly Dictionary<Keyboard.Key, MoveDirection> PLAYER_ONE = new Dictionary<Keyboard.Key, MoveDirection>()
                {
                    {Keyboard.Key.W, MoveDirection.UP },
                    {Keyboard.Key.S, MoveDirection.DOWN },
                    {Keyboard.Key.A, MoveDirection.LEFT },
                    {Keyboard.Key.D, MoveDirection.RIGHT }

                };

        public static readonly Dictionary<Keyboard.Key, MoveDirection> PLAYER_TWO = new Dictionary<Keyboard.Key, MoveDirection>()
                {
                    {Keyboard.Key.Up, MoveDirection.UP },
                    {Keyboard.Key.Down, MoveDirection.DOWN },
                    {Keyboard.Key.Left, MoveDirection.LEFT },
                    {Keyboard.Key.Right, MoveDirection.RIGHT }

                };
    }
}
