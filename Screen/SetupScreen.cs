using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SnakeGame.Engine;
using SnakeGame.GUI;
using SnakeGame.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using static SnakeGame.Screen.GameSetup;

namespace SnakeGame.Screen
{
    class SetupScreen : IState
    {
        private IGUIComponent guiContainer;
        private readonly Settings.Settings settings;
        private readonly GameSetup setup;

        private HumanPlayerSetup player1 = HumanPlayerSetup.PLAYER_ONE;
        private HumanPlayerSetup player2 = HumanPlayerSetup.PLAYER_TWO;

        uint roundCount = 3;

        public SetupScreen(GameEngine engine) : base(engine)
        {
            var manager = new AppConfigSettingsManager();
            settings = manager.Get();
            setup = new GameSetup();
        }

        public override void Init()
        {
            InitSetup();
            const float yOffset = 50f;

            var gui = GUIFactory.Instance;
            var guiBuilder = new GUIBuilder(gui);

            guiContainer = guiBuilder
               .Column(new Vector2f(engine.GetWindow().Size.X, engine.GetWindow().Size.Y - yOffset * 2))
                   .Component(gui.Text("Game setup"))
                   .Column(new Vector2f(engine.GetWindow().Size.X - 50, 60))
                       .Component(gui.Text("Players"))
                       .Row(size => new Vector2f(size.X, 25))
                           .Component(gui.Segmented(new Vector2f(engine.GetWindow().Size.X - 50, 25), new List<Button>()
                           {
                               gui.Button("Single", () => setup.Players = new List<PlayerSetup>{ player1 }),
                               gui.Button("Multi", () => setup.Players = new List<PlayerSetup> { player1, player2 }),
                               gui.Button("Bots", () => setup.Players = new List<PlayerSetup> { player1, BotPlayerSetup.BOT_SETUP })
                           }, 0))
                       .Close()
                   .Close()
                   .Column(new Vector2f(engine.GetWindow().Size.X - 50, 100))
                       .Component(gui.Text("Rounds"))
                       .Row(size => new Vector2f(size.X, 25))
                           .Component(gui.Segmented(new Vector2f(engine.GetWindow().Size.X - 50, 25), new List<Button>() 
                           {
                               gui.Button("Fast game", () => setup.RoundCount = 1),
                               SetupCountOption(gui.Button("Count", () => setup.RoundCount = 3)),
                               gui.Button("Infinite", () => setup.RoundCount = null) 
                           }, 0))
                       .Close()
                       .Row(size => new Vector2f(110, 20), LayoutContainer.AlignType.START)
                           .Component(gui.Button("-", () => ChangeRoundCount(-1)))
                           .Component(gui.Text(() => roundCount.ToString()))
                           .Component(gui.Button("+", () => ChangeRoundCount(1)))
                       .Close()
                   .Close()
                   .Row(new Vector2f(engine.GetWindow().Size.X - 50, 75), LayoutContainer.AlignType.END)
                      .Component(gui.Button("Back to menu", () => engine.GetMachine().PopState()))
                        .Component(gui.Button("Start", () => StartGame()))
                   .Close()
               .Close()
            .Build();

            guiContainer.Position = new Vector2f(0, yOffset);
        }

        private void ChangeRoundCount(int delta)
        {
            roundCount = (uint) Math.Max(roundCount + delta, 2);
        }

        private Button SetupCountOption(Button button)
        {
            button.HoverPadding = new Vector2f(50, 100);
            return button;
        }

        private void InitSetup()
        {
            setup.Difficulty = settings.Difficulty;
            setup.FieldSize = settings.FieldSize;
            InitPlayerSetup(player1, settings.Player1);
            InitPlayerSetup(player2, settings.Player2);
        }

        private void InitPlayerSetup(HumanPlayerSetup setup, PlayerSettings playerSettings)
        {
            setup.Name = playerSettings.Name;
            setup.Color = playerSettings.Color;
            setup.Bindings = playerSettings.Bindings.ToDictionary(entry => entry.Value, entry => entry.Key);
        }


        private void StartGame()
        {
            if (setup.RoundCount > 1)
            {
                setup.RoundCount = roundCount;
            }
            engine.GetMachine().PushState(new GameScreen(engine, setup));
        }

        public override void ProcessEvent(Event ev)
        {
            guiContainer.ProcessEvent(ev);
        }

        public override void Render(RenderTarget target, RenderStates states)
        {
            guiContainer.Render(target, states);
        }

        public override void Update(float dt)
        {
            guiContainer.Update(dt);
        }
    }
}
