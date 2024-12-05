using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace SnakeGame.Engine
{
    class GameEngine
    {
        private StateMachine machine;
        private RenderWindow window;
        private Vector2i mousePosition;

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

        public Vector2i GetMouseDelta<T>(Vector2i mousePos)
        {
            Vector2i delta = mousePos - mousePosition;
            mousePosition = mousePos;
            return delta;
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
