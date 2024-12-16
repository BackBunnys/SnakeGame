using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SnakeGame.Core;
using SnakeGame.Core.Player;
using SnakeGame.Engine;
using SnakeGame.GUI;
using System.Collections.Generic;
using static SnakeGame.Screen.GameSetup;

namespace SnakeGame.Screen
{
    class SettingsScreen : IState
    {
        private IGUIComponent guiContainer;
        private readonly GameSetup setup;
        Keyboard.Key key;

        public SettingsScreen(GameEngine engine, GameSetup setup) : base(engine)
        {
            this.setup = setup;
        }

        public override void Init()
        {
            const float yOffset = 50f;
            List<Color> availableColors = new List<Color>()
            {
                Color.White, Color.Blue, Color.Cyan, Color.Green, Color.Magenta, Color.Red
            };

            GUIFactory gui = GUIFactory.Instance;
            GUIBuilder guiBuilder = new GUIBuilder(gui);
            guiContainer = guiBuilder
                .Column(new Vector2f(engine.GetWindow().Size.X, engine.GetWindow().Size.Y - yOffset * 2))
                    .Component(gui.Text("Settings"))
                    .Row(new Vector2f(engine.GetWindow().Size.X, engine.GetWindow().Size.Y - yOffset * 2 - 200))
                        .Column(size => new Vector2f(size.X / 2, size.Y / 2 + 50))
                            .Component(gui.Text("Player 1"))
                            .Row(size => new Vector2f(size.X - 50, 40), LayoutContainer.AlignType.START)
                                .Component(gui.Text("Name"))
                                .Component(gui.Input(new ValueBinding<string>(() => setup.Players[0].Name, content => setup.Players[0].Name = content)))
                            .Close()
                            .Row(size => new Vector2f(size.X - 50, 40), LayoutContainer.AlignType.START)
                                .Component(gui.Text("Color"))
                                .Component(SetupColorPalette(gui.ColorPalette(availableColors, new ValueBinding<Color>(() => setup.Players[0].Color, color => setup.Players[0].Color = color))))
                            .Close()
                            .Row(size => new Vector2f(size.X - 50, 40), LayoutContainer.AlignType.START)
                                .Component(gui.Text("Controls"))
                                .Component(gui.Text("Preset"))
                            .Close()
                        .Close()
                        .Column(size => new Vector2f(size.X / 2, size.Y / 2 + 125))
                            .Component(gui.Text("Player 2"))
                            .Row(size => new Vector2f(size.X - 50, 40), LayoutContainer.AlignType.START)
                                .Component(gui.Text("Name"))
                                .Component(gui.Input(new ValueBinding<string>(() => "Player 2", content => { })))
                            .Close()
                            .Row(size => new Vector2f(size.X - 50, 40), LayoutContainer.AlignType.START)
                                .Component(gui.Text("Color"))
                                .Component(gui.Text("Yellow"))
                            .Close()
                            .Row(size => new Vector2f(size.X - 50, 40), LayoutContainer.AlignType.START)
                                .Component(gui.Text("Controls"))
                                .Component(gui.Text("Preset"))
                            .Close()
                            .Row(size => new Vector2f(50, 50))
                                .Component(gui.BindingButton(new ValueBinding<Keyboard.Key>(() => key, key => this.key = key)))
                            .Close()
                            .Row(size => new Vector2f(180, 50))
                                .Component(gui.BindingButton(new ValueBinding<Keyboard.Key>(() => key, key => this.key = key)))
                                .Component(gui.BindingButton(new ValueBinding<Keyboard.Key>(() => key, key => this.key = key)))
                                .Component(gui.BindingButton(new ValueBinding<Keyboard.Key>(() => key, key => this.key = key)))
                            .Close()
                        .Close()
                    .Close()
                    .Column(new Vector2f(engine.GetWindow().Size.X - 50, 75))
                        .Component(gui.Text("Difficulty"))
                        .Row(size => new Vector2f(size.X, 25))
                            .Component(gui.Button("EASY", () => setup.Difficulty = GameDifficulty.EASY))
                            .Component(gui.Button("NORMAL", () => setup.Difficulty = GameDifficulty.NORMAL))
                            .Component(gui.Button("HARD", () => setup.Difficulty = GameDifficulty.HARD))
                        .Close()
                    .Close()
                    .Row(new Vector2f(engine.GetWindow().Size.X - 50, 75), LayoutContainer.AlignType.END)
                        .Component(gui.Button("Back to menu", () => engine.GetMachine().PopState()))
                        .Component(gui.Button("Save", () => { }))
                    .Close()
                .Close()
            .Build();

            guiContainer.Position = new Vector2f(0, yOffset);
        }

        private ColorPalette SetupColorPalette(ColorPalette colorPalette)
        {
            colorPalette.Size = new Vector2f(200, 25);
            colorPalette.ColorTileMargin = 10;
            colorPalette.ColorTileSize = new Vector2f(25, 25);
            return colorPalette;
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
