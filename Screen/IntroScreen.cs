using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SnakeGame.Engine;
using System;

namespace SnakeGame.Screen
{
    class IntroScreen : IState
    {
        Text applicationName;
        Text applicationVersion;
        readonly int introDurationInSeconds = 3;
        Clock clock;
        readonly GameSetup gameSetup = new GameSetup();
        string roundCountInput = "";

        public IntroScreen(GameEngine engine) : base(engine)
        {
        }

        public override void Init()
        {
            Font font = new Font(Resource.arial);
            applicationName = new Text(Resource.ApplicationName, font)
            {
                CharacterSize = 32,
                FillColor = Color.Black,
            };
            applicationName.Position = new Vector2f(engine.GetWindow().Size.X / 2 - applicationName.GetLocalBounds().Width / 2, engine.GetWindow().Size.Y / 2 - applicationName.GetLocalBounds().Height);
            applicationVersion = new Text("Version: " + Resource.ApplicationVersion, font)
            {
                CharacterSize = 14,
                FillColor = new Color(100, 100, 100),
            };
            applicationVersion.Position = new Vector2f(engine.GetWindow().Size.X / 2 - applicationName.GetGlobalBounds().Width / 4, engine.GetWindow().Size.Y - applicationName.GetLocalBounds().Height * 2);

            clock = new Clock();
        }

        public override void ProcessEvent(Event ev)
        {
            if (ev.Type == EventType.KeyPressed)
            {
                if (ev.Key.Code == Keyboard.Key.S) gameSetup.Type = GameSetup.GameType.SINGLEPLAYER;
                if (ev.Key.Code == Keyboard.Key.M) gameSetup.Type = GameSetup.GameType.MULTIPLAYER;
                if (ev.Key.Code == Keyboard.Key.B) gameSetup.VersusBot = true;
                if (ev.Key.Code == Keyboard.Key.P) gameSetup.VersusBot = false;
                if ((uint) ev.Key.Code >= (uint) Keyboard.Key.Num0 && (uint) ev.Key.Code <= (uint) Keyboard.Key.Num9) roundCountInput += ev.Key.Code.ToString().Replace("Num", "");
            }
        }

        public override void Render(RenderTarget target, RenderStates states)
        {
            target.Draw(applicationName, states);
            target.Draw(applicationVersion, states);

        }

        public override void Update(float dt)
        {
            if (clock.ElapsedTime.AsSeconds() > introDurationInSeconds)
            {
                StartGame();
            }
            applicationName.FillColor = new Color(0, 0, 0, (byte) (255 - clock.ElapsedTime.AsMilliseconds() / 3000.0 * 255));
            applicationVersion.FillColor = new Color(100, 100, 100, (byte) (255 - clock.ElapsedTime.AsMilliseconds() / 3000.0 * 255));
        }

        private void StartGame()
        {
            PrepareGameSetup();
            engine.GetMachine().ReplaceState(new GameScreen(engine, gameSetup));
        }

        private void PrepareGameSetup()
        {
            if (roundCountInput.Length == 0) return;

            if (uint.TryParse(roundCountInput, out uint roundCount))
            {
                gameSetup.RoundCount = Math.Max(roundCount, 1);
            }
        }
    }
}
