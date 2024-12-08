using SFML.Window;
using System.Collections.Generic;

namespace SnakeGame.Core.Controller
{
    class SnakeKeyboardController
    {
        public SnakeKeyboardController(Dictionary<Keyboard.Key, MoveDirection> bindings, Snake snake)
        {
            Bindings = bindings;
            Snake = snake;
        }

        public Dictionary<Keyboard.Key, MoveDirection> Bindings { get; set; }
        public Snake Snake { get; }

        public void OnKeyPressed(Keyboard.Key key)
        {
            if (Bindings.TryGetValue(key, out MoveDirection moveDirection))
            {
                if (Snake.Direction == moveDirection)
                {
                    Snake.Speed = MoveSpeed.FAST;
                } 
                else if (Snake.Direction.IsOpposite(moveDirection)) {
                    Snake.Speed = MoveSpeed.SLOW;
                }
                else
                {
                    Snake.Direction = moveDirection;
                }
            }
        }

        public void OnKeyReleased(Keyboard.Key key)
        {
            if (Bindings.ContainsKey(key))
            {
                Snake.Speed = MoveSpeed.NORMAL;
            }
        }
    }
}
