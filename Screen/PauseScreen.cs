using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SnakeGame.Core;
using SnakeGame.Engine;
using SnakeGame.Utils;

namespace SnakeGame.Screen
{
    class PauseScreen : OverlayScreen
    {
        Text pauseText;
        Text continueText;
        Text restartText;
        Text exitText;
        private readonly GameSetup setup;

        public PauseScreen(GameEngine engine, GameSetup setup) : base(engine)
        {
            this.setup = setup;
        }

        public override void Init()
        {
            base.Init();
            Font font = new Font(Resources.arial);
            pauseText = new Text("PAUSE", font)
            {
                FillColor = Color.White,
                CharacterSize = 32,
                Style = Text.Styles.Bold
            };
            pauseText.Position = new Vector2f(engine.GetWindow().Size.X / 2 - pauseText.GetLocalBounds().Width / 2, engine.GetWindow().Size.Y / 6);

            continueText = new Text("Continue", font)
            {
                FillColor = Color.White,
                CharacterSize = 24,
                Style = Text.Styles.Bold
            };
            continueText.Position = new Vector2f(engine.GetWindow().Size.X / 2 - continueText.GetLocalBounds().Width / 2, engine.GetWindow().Size.Y / 6 * 2);
            restartText = new Text("Restart", font)
            {
                FillColor = Color.White,
                CharacterSize = 24,
                Style = Text.Styles.Bold
            };
            restartText.Position = new Vector2f(engine.GetWindow().Size.X / 2 - restartText.GetLocalBounds().Width / 2, engine.GetWindow().Size.Y / 6 * 3);
            exitText = new Text("Exit", font)
            {
                FillColor = Color.White,
                CharacterSize = 24,
                Style = Text.Styles.Bold
            };
            exitText.Position = new Vector2f(engine.GetWindow().Size.X / 2 - exitText.GetLocalBounds().Width / 2, engine.GetWindow().Size.Y / 6 * 4);
        }

        public override void Update(float dt)
        {
            //Do not updates
        }

        public override void ProcessEvent(Event ev)
        {
            if (ev.Type == EventType.KeyPressed && ev.Key.Code == Keyboard.Key.Escape)
            {
                engine.GetMachine().PopState();
            }
            if (ev.Type == EventType.MouseButtonReleased && ev.MouseButton.Button == Mouse.Button.Left)
            {
                if (continueText.GetGlobalBounds().Inflate(new Vector2f(50, 50)).Contains(ev.MouseButton.X, ev.MouseButton.Y))
                {
                    engine.GetMachine().PopState();
                }
                if (restartText.GetGlobalBounds().Inflate(new Vector2f(50, 50)).Contains(ev.MouseButton.X, ev.MouseButton.Y))
                {
                    engine.GetMachine().Clear();
                    engine.GetMachine().PushState(new GameScreen(engine, setup));
                }
                if (exitText.GetGlobalBounds().Inflate(new Vector2f(50, 50)).Contains(ev.MouseButton.X, ev.MouseButton.Y))
                {
                    engine.GetMachine().Clear();
                }
            }
            if (ev.Type == EventType.MouseMoved)
            {
                continueText.FillColor = Color.White;
                restartText.FillColor = Color.White;
                exitText.FillColor = Color.White;
                if (continueText.GetGlobalBounds().Inflate(new Vector2f(50, 50)).Contains(ev.MouseMove.X, ev.MouseMove.Y))
                {
                    continueText.FillColor = Color.Yellow;
                }
                if (restartText.GetGlobalBounds().Inflate(new Vector2f(50, 50)).Contains(ev.MouseMove.X, ev.MouseMove.Y))
                {
                    restartText.FillColor = Color.Yellow;
                }
                if (exitText.GetGlobalBounds().Inflate(new Vector2f(50, 50)).Contains(ev.MouseMove.X, ev.MouseMove.Y))
                {
                    exitText.FillColor = Color.Yellow;
                }
            }
        }

        public override void Render(RenderTarget target, RenderStates states)
        {
            base.Render(target, states);
            target.Draw(pauseText);
            target.Draw(continueText);
            target.Draw(restartText);
            target.Draw(exitText);
        }
    }
}
