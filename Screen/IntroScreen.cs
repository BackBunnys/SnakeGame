using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SnakeGame.Engine;
using System;

namespace SnakeGame.Screen
{
    class IntroScreen : IState
    {

        Text text;
        Clock clock;

        public IntroScreen(Engine.GameEngine appData) : base(appData)
        {
        }

        public override void Init()
        {
            text = new Text("Snake game", new Font(Resource.arial));
            text.CharacterSize = 32;
            text.FillColor = Color.Black;
            text.Position = new Vector2f(engine.GetWindow().Size.X / 2 - text.GetLocalBounds().Width / 2, engine.GetWindow().Size.Y / 2 - text.GetLocalBounds().Height);

            clock = new Clock();
        }

        public override void ProcessEvent(Event ev)
        {
        }

        public override void Render(RenderTarget target)
        {
            target.Draw(text);
        }

        public override void Update()
        {
            if (clock.ElapsedTime.AsSeconds() > 3)
            {
                engine.GetMachine().PushState(new GameScreen(engine));
            }
            text.FillColor = new Color(0, 0, 0, (byte) (255 - (clock.ElapsedTime.AsMilliseconds() / 3000.0) * 255));
        }
    }
}
