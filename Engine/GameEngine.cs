using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace SnakeGame.Engine
{
    class GameEngine
    {
        private StateMachine machine;
        private RenderWindow window;

        public GameEngine(VideoMode videoMode)
        {
            InitWindow(videoMode);
            InitMachine();
        }

        private void InitMachine()
        {
            machine = new StateMachine();
        }

        private void InitWindow(VideoMode videoMode)
        {
            window = new RenderWindow(videoMode, "Snake");
            window.SetFramerateLimit(60);
        }

        public RenderWindow GetWindow()
        {
            return window;
        }

        public StateMachine GetMachine()
        {
            return machine;
        }
    }
}
