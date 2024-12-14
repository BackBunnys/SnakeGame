using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SnakeGame.Engine;
using SnakeGame.GUI;

namespace SnakeGame.Screen
{
    class MenuScreen : IState
    {
        Container guiContainer;
        GameSetup setup;

        public MenuScreen(GameEngine engine, GameSetup setup) : base(engine)
        {
            this.setup = setup;
        }

        public override void Init()
        {
            var yOffset = 75;
            GUIFactory gui = GUIFactory.Instance;
            guiContainer = gui.Container();
            guiContainer.Size = new Vector2f(engine.GetWindow().Size.X, engine.GetWindow().Size.Y - yOffset * 2);
            guiContainer.Position = new Vector2f(0, yOffset);

            var title = gui.Text("Snake Game");
            title.CharacterSize = 48;
            guiContainer.Components.Add(title);

            var buttonsContainer = gui.Container();
            buttonsContainer.Size = new Vector2f(engine.GetWindow().Size.X, engine.GetWindow().Size.Y / 2);
            buttonsContainer.Components.Add(gui.Button("Start", () => engine.GetMachine().PushState(new GameScreen(engine, setup))));
            buttonsContainer.Components.Add(gui.Button("Settings", () => engine.GetMachine().PushState(new SettingsScreen(engine, setup))));
            buttonsContainer.Components.Add(gui.Button("Exit", () => engine.GetMachine().Clear()));

            guiContainer.Components.Add(buttonsContainer);
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
