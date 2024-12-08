
using SFML.Window;
using SnakeGame.Core.Controller;

namespace SnakeGame.Core.Player
{
    class BotPlayer : Player
    {
        public SnakeBotController Controller { get; }

        public BotPlayer(Snake snake, SnakeBotController controller) : base(snake)
        {
            Controller = controller;
        }

        public override void ProcessEvent(Event ev)
        {
            
        }

        public override void Update(float dt)
        {
            Controller.Update();
            base.Update(dt);
        }
    }
}
