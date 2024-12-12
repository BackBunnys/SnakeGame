using SFML.Graphics;
using SFML.Window;

namespace SnakeGame.Core.Player
{
    abstract class Player
    {
        public string Name { get; set; }
        public Color Color { get => Snake.Color; set => Snake.Color = value; }
        public uint Score { get; set; } = 0;
        public Snake Snake { get; private set; }

        protected Player(Snake snake)
        {
            Snake = snake;
        }

        public virtual void Update(float dt)
        {
            Snake.Update(dt);
        }
        public abstract void ProcessEvent(Event ev);
        public virtual void Render(RenderTarget target, RenderStates states)
        {
            Snake.Render(target, states);
        }
    }
}
