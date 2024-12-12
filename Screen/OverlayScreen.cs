using SFML.Graphics;
using SFML.System;
using SnakeGame.Engine;

namespace SnakeGame.Screen
{
    abstract class OverlayScreen : IState
    {
        private readonly IState belowState;
        protected RectangleShape overlay;

        protected OverlayScreen(GameEngine engine) : base(engine)
        {
            belowState = engine.GetMachine().GetCurrentState();
        }

        public override void Init()
        {
            overlay = new RectangleShape(new Vector2f(engine.GetWindow().Size.X, engine.GetWindow().Size.Y))
            {
                FillColor = new Color(0, 0, 0, 100)
            };
        }

        public override void Render(RenderTarget target, RenderStates states)
        {
            belowState.Render(target, states);
            target.Draw(overlay);
        }
    }
}
