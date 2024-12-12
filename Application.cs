using SFML.Graphics;
using SFML.Window;
using SnakeGame.Screen;
using SnakeGame.Engine;
using System;
using SnakeGame.Utils;
using SFML.System;

namespace SnakeGame
{
    class Application
    {
        private readonly GameEngine engine;
        private readonly RenderStates states;
        private readonly Sprite background;

        void OnClose(object sender, EventArgs e)
        {
            engine.GetMachine().Clear();
        }

        void OnKeyPressed(object sender, KeyEventArgs e)
        {
            Event ev = new Event
            {
                Type = EventType.KeyPressed
            };
            ev.Key.Code = e.Code;
            engine.GetMachine().GetCurrentState().ProcessEvent(ev);
        }
        void OnKeyReleased(object sender, KeyEventArgs e)
        {
            Event ev = new Event
            {
                Type = EventType.KeyReleased
            };
            ev.Key.Code = e.Code;
            engine.GetMachine().GetCurrentState().ProcessEvent(ev);
        }
        void OnMouseButtonReleased(object sender, MouseButtonEventArgs e)
        {
            Event ev = new Event
            {
                Type = EventType.MouseButtonReleased
            };
            ev.MouseButton.Button = e.Button;
            ev.MouseButton.X = e.X;
            ev.MouseButton.Y = e.Y;
            engine.GetMachine().GetCurrentState().ProcessEvent(ev);
        }
        void OnMouseMove(object sender, MouseMoveEventArgs e)
        {
            Event ev = new Event
            {
                Type = EventType.MouseMoved
            };
            ev.MouseMove.X = e.X;
            ev.MouseMove.Y = e.Y;
            engine.GetMachine().GetCurrentState().ProcessEvent(ev);
        }

        public Application(VideoMode videoMode)
        {
            engine = new GameEngine(videoMode);
            engine.GetMachine().PushState(new IntroScreen(engine));

            engine.GetWindow().SetVerticalSyncEnabled(true);

            background = new Sprite(Resources.simple_background);
            background.Scale = new Vector2f(videoMode.Width / background.GetLocalBounds().Width, videoMode.Height / background.GetLocalBounds().Height);
            states = RenderStates.Default;
            BindEvents();
        }

        private void BindEvents()
        {
            engine.GetWindow().Closed += OnClose;
            engine.GetWindow().KeyPressed += OnKeyPressed;
            engine.GetWindow().KeyReleased += OnKeyReleased;
            engine.GetWindow().MouseButtonReleased += OnMouseButtonReleased;
            engine.GetWindow().MouseMoved += OnMouseMove;
        }

        private void Update(float dt)
        {
            engine.GetWindow().DispatchEvents();
            if (!engine.GetMachine().IsEmpty())
            {
                engine.GetMachine().GetCurrentState().Update(dt);
            }
        }

        private void Render()
        {
            if (engine.GetWindow().IsOpen)
            {
                engine.GetWindow().Clear(Color.White);
                engine.GetWindow().Draw(background);

                if (!engine.GetMachine().IsEmpty())
                    engine.GetMachine().GetCurrentState().Render(engine.GetWindow(), states);

                engine.GetWindow().Display();
            }
        }

        public void Run()
        {
            Clock clock = new Clock();
            while (!engine.GetMachine().IsEmpty())
            {
                float dt = clock.ElapsedTime.AsMilliseconds();
                clock.Restart();
                Update(dt);
                Render();
            }
        }
    }
}
