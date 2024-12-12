using SFML.Window;
using SnakeGame.Core.Controller;

namespace SnakeGame.Core.Player
{
    class HumanPlayer : Player
    {
        SnakeKeyboardController Controller { get; }

        public HumanPlayer(Snake snake, SnakeKeyboardController controller) : base(snake)
        {
            Controller = controller;
        }

        public override void ProcessEvent(Event ev)
        {
            if (ev.Type == EventType.KeyPressed)
            {
                Controller.OnKeyPressed(ev.Key.Code);
            }
            if (ev.Type == EventType.KeyReleased)
            {
                Controller.OnKeyReleased(ev.Key.Code);
            }
        }
    }
}
