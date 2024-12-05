using SFML.Graphics;
using SFML.Window;
using SnakeGame.Screen;
using SnakeGame.Engine;
using System;

namespace SnakeGame
{
    class Application
    {
        private GameEngine engine;

        void OnClose(object sender, EventArgs e)
        {
            RenderWindow window = (RenderWindow) sender;
            window.Close();
        }

        public Application(VideoMode videoMode)
        {
            engine = new GameEngine(videoMode);
            engine.GetMachine().PushState(new IntroScreen(engine));
            engine.GetWindow().Closed += new EventHandler(OnClose);
            engine.GetWindow().SetVerticalSyncEnabled(true);
        }

        private void Update()
        {
            engine.GetWindow().DispatchEvents();
            if (!engine.GetMachine().IsEmpty())
                engine.GetMachine().GetCurrentState().Update();
        }

        private void Render()
        {
            if (engine.GetWindow().IsOpen)
            {
                engine.GetWindow().Clear(Color.White);

                if (!engine.GetMachine().IsEmpty())
                    engine.GetMachine().GetCurrentState().Render(engine.GetWindow());

                engine.GetWindow().Display();
            }
        }

        public void Run()
        {
            while (!engine.GetMachine().IsEmpty())
            {
                Update();
                Render();
            }
        }
    }
}
