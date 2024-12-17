using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SnakeGame.Core;
using SnakeGame.Engine;
using SnakeGame.GUI;
using SnakeGame.Settings;
using System;
using System.Collections.Generic;
using static SnakeGame.Screen.GameSetup;

namespace SnakeGame.Screen
{
    class SettingsScreen : IState
    {
        private IGUIComponent guiContainer;
        private readonly ISettingsManager manager;
        private readonly Settings.Settings settings;

        public SettingsScreen(GameEngine engine) : base(engine)
        {
            manager = new AppConfigSettingsManager();
            settings = manager.Get();
        }

        public override void Init()
        {
            const float yOffset = 25f;
            List<Color> availableColors = new List<Color>()
            {
                Color.Blue, Color.Cyan, Color.Green, Color.Magenta, Color.Red
            };

            GUIFactory gui = GUIFactory.Instance;
            GUIBuilder guiBuilder = new GUIBuilder(gui);
            guiContainer = guiBuilder
                .Column(new Vector2f(engine.GetWindow().Size.X, engine.GetWindow().Size.Y - yOffset * 2))
                    .Component(gui.Text("Settings"))
                    .Row(new Vector2f(engine.GetWindow().Size.X, engine.GetWindow().Size.Y - yOffset * 2 - 200))
                        .Column(size => new Vector2f(size.X / 2, size.Y / 2 + 125))
                            .Component(gui.Text("Player 1"))
                            .Row(size => new Vector2f(size.X - 50, 40), LayoutContainer.AlignType.START)
                                .Component(gui.Text("Name"))
                                .Component(gui.Input(new ValueBinding<string>(() => settings.Player1.Name, content => settings.Player1.Name = content)))
                            .Close()
                            .Row(size => new Vector2f(size.X - 50, 40), LayoutContainer.AlignType.START)
                                .Component(gui.Text("Color"))
                                .Component(SetupColorPalette(gui.ColorPalette(availableColors, new ValueBinding<Color>(() => settings.Player1.Color, color => settings.Player1.Color = color))))
                            .Close()
                            .Row(size => new Vector2f(size.X - 50, 40), LayoutContainer.AlignType.START)
                                .Component(gui.Text("Controls"))
                                .Row(new Vector2f(150, 30))
                                    .Component(SetupPresetButton(gui.Button("WASD", () => ApplyWASDPreset(settings.Player1))))
                                    .Component(SetupPresetButton(gui.Button("Arrows", () => ApplyArrowsPreset(settings.Player1))))
                                .Close()
                            .Close()
                            .Row(size => new Vector2f(50, 50))
                                .Component(gui.BindingButton(new ValueBinding<Keyboard.Key>(() => settings.Player1.Bindings[MoveDirection.UP], key => settings.Player1.Bindings[MoveDirection.UP] = key)))
                            .Close()
                            .Row(size => new Vector2f(180, 50))
                                .Component(gui.BindingButton(new ValueBinding<Keyboard.Key>(() => settings.Player1.Bindings[MoveDirection.LEFT], key => settings.Player1.Bindings[MoveDirection.LEFT] = key)))
                                .Component(gui.BindingButton(new ValueBinding<Keyboard.Key>(() => settings.Player1.Bindings[MoveDirection.DOWN], key => settings.Player1.Bindings[MoveDirection.DOWN] = key)))
                                .Component(gui.BindingButton(new ValueBinding<Keyboard.Key>(() => settings.Player1.Bindings[MoveDirection.RIGHT], key => settings.Player1.Bindings[MoveDirection.RIGHT] = key)))
                            .Close()
                        .Close()
                        .Column(size => new Vector2f(size.X / 2, size.Y / 2 + 125))
                            .Component(gui.Text("Player 2"))
                            .Row(size => new Vector2f(size.X - 50, 40), LayoutContainer.AlignType.START)
                                .Component(gui.Text("Name"))
                                .Component(gui.Input(new ValueBinding<string>(() => settings.Player2.Name, content => settings.Player2.Name = content)))
                            .Close()
                            .Row(size => new Vector2f(size.X - 50, 40), LayoutContainer.AlignType.START)
                                .Component(gui.Text("Color"))
                                .Component(SetupColorPalette(gui.ColorPalette(availableColors, new ValueBinding<Color>(() => settings.Player2.Color, color => settings.Player2.Color = color))))
                            .Close()
                            .Row(size => new Vector2f(size.X - 50, 40), LayoutContainer.AlignType.START)
                                .Component(gui.Text("Controls"))
                                .Row(new Vector2f(150, 30))
                                    .Component(SetupPresetButton(gui.Button("WASD", () => ApplyWASDPreset(settings.Player2))))
                                    .Component(SetupPresetButton(gui.Button("Arrows", () => ApplyArrowsPreset(settings.Player2))))
                                .Close()
                            .Close()
                            .Row(size => new Vector2f(50, 50))
                                .Component(gui.BindingButton(new ValueBinding<Keyboard.Key>(() => settings.Player2.Bindings[MoveDirection.UP], key => settings.Player2.Bindings[MoveDirection.UP] = key)))
                            .Close()
                            .Row(size => new Vector2f(180, 50))
                                .Component(gui.BindingButton(new ValueBinding<Keyboard.Key>(() => settings.Player2.Bindings[MoveDirection.LEFT], key => settings.Player2.Bindings[MoveDirection.LEFT] = key)))
                                .Component(gui.BindingButton(new ValueBinding<Keyboard.Key>(() => settings.Player2.Bindings[MoveDirection.DOWN], key => settings.Player2.Bindings[MoveDirection.DOWN] = key)))
                                .Component(gui.BindingButton(new ValueBinding<Keyboard.Key>(() => settings.Player2.Bindings[MoveDirection.RIGHT], key => settings.Player2.Bindings[MoveDirection.RIGHT] = key)))
                            .Close()
                        .Close()
                    .Close()
                    .Column(new Vector2f(engine.GetWindow().Size.X - 50, 60))
                        .Component(gui.Text("Field"))
                        .Row(size => new Vector2f(250, 25), LayoutContainer.AlignType.END)
                            .Row(size => new Vector2f(100, 25), LayoutContainer.AlignType.START)
                                .Component(gui.Button("-", () => ChangeFieldSize(new Vector2i(-1, 0))))
                                .Component(gui.Text(() => settings.FieldSize.X.ToString()))
                                .Component(gui.Button("+", () => ChangeFieldSize(new Vector2i(1, 0))))
                            .Close()
                            .Row(size => new Vector2f(100, 25), LayoutContainer.AlignType.START)
                                .Component(gui.Button("-", () => ChangeFieldSize(new Vector2i(0, -1))))
                                .Component(gui.Text(() => settings.FieldSize.Y.ToString()))
                                .Component(gui.Button("+", () => ChangeFieldSize(new Vector2i(0, 1))))
                            .Close()
                        .Close()
                    .Close()
                    .Row(new Vector2f(engine.GetWindow().Size.X - 50, 60)).Close()
                    .Column(new Vector2f(engine.GetWindow().Size.X - 50, 60))
                        .Component(gui.Text("Difficulty"))
                        .Row(size => new Vector2f(size.X, 25))
                            .Component(gui.Segmented(new Vector2f(engine.GetWindow().Size.X - 50, 25), new List<Button>()
                            {
                                gui.Button("EASY", () => settings.Difficulty = GameDifficulty.EASY),
                                gui.Button("NORMAL", () => settings.Difficulty = GameDifficulty.NORMAL),
                                gui.Button("HARD", () => settings.Difficulty = GameDifficulty.HARD)
                            }, settings.Difficulty == GameDifficulty.EASY ? 0 : settings.Difficulty == GameDifficulty.NORMAL ? 1 : 2))
                        .Close()
                    .Close()
                    .Row(new Vector2f(engine.GetWindow().Size.X - 50, 75), LayoutContainer.AlignType.END)
                        .Component(gui.Button("Back to menu", () => engine.GetMachine().PopState()))
                        .Component(gui.Button("Save", () => manager.Save(settings)))
                    .Close()
                .Close()
            .Build();

            guiContainer.Position = new Vector2f(0, yOffset);
        }

        private void ChangeFieldSize(Vector2i delta)
        {
            settings.FieldSize = new Vector2u((uint)Math.Max(settings.FieldSize.X + delta.X, 10), (uint)Math.Max(settings.FieldSize.Y + delta.Y, 10));
        }

        private ColorPalette SetupColorPalette(ColorPalette colorPalette)
        {
            colorPalette.Size = new Vector2f(170, 25);
            colorPalette.ColorTileMargin = 10;
            colorPalette.ColorTileSize = new Vector2f(25, 25);
            return colorPalette;
        }

        private Button SetupPresetButton(Button button)
        {
            button.Text.CharacterSize = 13;
            button.HoverPadding = new Vector2f(5, 5);
            return button;
        }

        private void ApplyWASDPreset(PlayerSettings player)
        {
            player.Bindings[MoveDirection.UP] = Keyboard.Key.W;
            player.Bindings[MoveDirection.DOWN] = Keyboard.Key.S;
            player.Bindings[MoveDirection.LEFT] = Keyboard.Key.A;
            player.Bindings[MoveDirection.RIGHT] = Keyboard.Key.D;
        }

        private void ApplyArrowsPreset(PlayerSettings player)
        {
            player.Bindings[MoveDirection.UP] = Keyboard.Key.Up;
            player.Bindings[MoveDirection.DOWN] = Keyboard.Key.Down;
            player.Bindings[MoveDirection.LEFT] = Keyboard.Key.Left;
            player.Bindings[MoveDirection.RIGHT] = Keyboard.Key.Right;
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
