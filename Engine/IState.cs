using SFML.Graphics;
using SFML.Window;

namespace SnakeGame.Engine
{
    abstract class IState
    {
        protected GameEngine engine;

        public string Description { get; protected set; } = string.Empty;

        protected IState(GameEngine engine)
        {
            this.engine = engine;
        }

        public abstract void Init();

        public abstract void ProcessEvent(Event ev);
        public abstract void Update();
        public abstract void Render(RenderTarget target);

        public virtual string GetDescription()
        {
            return Description;
        }
    }
}
