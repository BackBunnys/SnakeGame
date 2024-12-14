using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SnakeGame.Engine;
using SnakeGame.GUI;

namespace SnakeGame.Screen
{
    class SettingsScreen : IState
    {
        Container guiContainer;
        GameSetup setup;

        public SettingsScreen(GameEngine engine, GameSetup setup) : base(engine)
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

            var title = gui.Text("Settings");
            title.CharacterSize = 48;
            guiContainer.Components.Add(title);

            var configContainer = gui.Container();
            configContainer.Direction = Container.DirectionType.HORIZONTAL;
            configContainer.Size = new Vector2f(engine.GetWindow().Size.X, engine.GetWindow().Size.Y - yOffset * 2 - 150);

            var player1Container = gui.Container();
            player1Container.Size = new Vector2f(engine.GetWindow().Size.X / 2, configContainer.Size.Y);
            player1Container.Components.Add(gui.Text("Player 1"));
            var player1ConfigContainer = gui.Container();
            player1ConfigContainer.Align = Container.AlignType.START;
            player1ConfigContainer.Size = new Vector2f(player1Container.Size.X - 50, player1Container.Size.Y - 50);

            var player1NameContainer = gui.Container();
            player1NameContainer.Direction = Container.DirectionType.HORIZONTAL;
            player1NameContainer.Size = new Vector2f(player1ConfigContainer.Size.X, 50);
            player1NameContainer.Components.Add(gui.Text("Name"));
            player1NameContainer.Components.Add(gui.Text(setup.Players[0].Name));

            player1ConfigContainer.Components.Add(player1NameContainer);

            player1Container.Components.Add(player1ConfigContainer);

            var player2Container = gui.Container();
            player2Container.Size = player1Container.Size;
            player2Container.Components.Add(gui.Text("Player 2"));
            var player2ConfigContainer = gui.Container();
            player2ConfigContainer.Align = Container.AlignType.START;
            player2ConfigContainer.Size = new Vector2f(player2Container.Size.X - 50, player2Container.Size.Y - 50);
            player2ConfigContainer.Components.Add(gui.Text("Name"));

            player2Container.Components.Add(player2ConfigContainer);

            configContainer.Components.Add(player1Container);
            configContainer.Components.Add(player2Container);
            guiContainer.Components.Add(configContainer);

            var navigationsContainer = gui.Container();
            navigationsContainer.Direction = Container.DirectionType.HORIZONTAL;
            navigationsContainer.Size = new Vector2f(engine.GetWindow().Size.X - 50, 50);
            navigationsContainer.Components.Add(gui.Button("Back to menu", () => engine.GetMachine().PopState()));
            navigationsContainer.Components.Add(gui.Button("Save", () => { }));

            guiContainer.Components.Add(navigationsContainer);
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
