using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SnakeGame.Engine;
using SnakeGame.GUI;

namespace SnakeGame.Screen
{
    class MenuScreen : IState
    {
        IGUIComponent guiContainer;

        public MenuScreen(GameEngine engine) : base(engine)
        {
        }

        public override void Init()
        {
            var yOffset = 75;
            GUIFactory gui = GUIFactory.Instance;
            var guiBuilder = new GUIBuilder(gui);
            guiContainer = guiBuilder
                .Column(new Vector2f(engine.GetWindow().Size.X, engine.GetWindow().Size.Y * 0.6f))
                    .Component(SetupTitle(gui.Text("Snake Game")))
                    .Column(size => new Vector2f(size.X, size.Y * 0.6f))
                        .Component(gui.Button("Start", () => engine.GetMachine().PushState(new SetupScreen(engine))))
                        .Component(gui.Button("Settings", () => engine.GetMachine().PushState(new SettingsScreen(engine))))
                        .Component(gui.Button("Exit", () => engine.GetMachine().Clear()))
                    .Close()
                .Close()
                .Build();
            guiContainer.Position = new Vector2f(0, yOffset);
        }

        private GUI.Text SetupTitle(GUI.Text text)
        {
            text.CharacterSize = 48;
            return text;
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
