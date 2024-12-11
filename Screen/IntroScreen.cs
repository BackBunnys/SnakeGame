using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SnakeGame.Engine;
using SnakeGame.Utils;
using System;
using System.Collections.Generic;
using static SnakeGame.Screen.GameSetup;

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
            Font font = Resources.arial;
            applicationName = new Text(Resource.ApplicationName, font)
            {
                CharacterSize = 48,
                FillColor = Color.White
            };
            applicationName.Position = new Vector2f(engine.GetWindow().Size.X / 2 - applicationName.GetLocalBounds().Width / 2, engine.GetWindow().Size.Y / 2 - applicationName.GetLocalBounds().Height);
            applicationVersion = new Text("Version: " + Resource.ApplicationVersion, font)
            {
                CharacterSize = 14,
                FillColor = Color.White,
            };
            applicationVersion.Position = new Vector2f(engine.GetWindow().Size.X / 2 - applicationVersion.GetGlobalBounds().Width / 2, engine.GetWindow().Size.Y - applicationVersion.GetLocalBounds().Height * 4);

            clock = new Clock();
        }

        public override void ProcessEvent(Event ev)
        {
            if (ev.Type == EventType.KeyPressed)
            {
                if (ev.Key.Code == Keyboard.Key.S) gameSetup.Players = new List<PlayerSetup>() { HumanPlayerSetup.PLAYER_ONE };
                if (ev.Key.Code == Keyboard.Key.M) gameSetup.Players = new List<PlayerSetup>() { HumanPlayerSetup.PLAYER_ONE, HumanPlayerSetup.PLAYER_TWO };
                if (ev.Key.Code == Keyboard.Key.B) gameSetup.Players = new List<PlayerSetup>() { HumanPlayerSetup.PLAYER_ONE, BotPlayerSetup.BOT_SETUP };
                if ((uint) ev.Key.Code >= (uint) Keyboard.Key.Num0 && (uint) ev.Key.Code <= (uint) Keyboard.Key.Num9) roundCountInput += ev.Key.Code.ToString().Replace("Num", "");
                if (ev.Key.Code == Keyboard.Key.E) gameSetup.Difficulty = GameDifficulty.EASY;
                if (ev.Key.Code == Keyboard.Key.N) gameSetup.Difficulty = GameDifficulty.NORMAL;
                if (ev.Key.Code == Keyboard.Key.H) gameSetup.Difficulty = GameDifficulty.HARD;
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
            applicationName.FillColor = new Color(255, 255, 255, (byte) (255 - clock.ElapsedTime.AsMilliseconds() / 3000.0 * 255));
            applicationVersion.FillColor = new Color(255, 255, 255, (byte) (255 - clock.ElapsedTime.AsMilliseconds() / 3000.0 * 255));
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
