using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SnakeGame.Engine;
using SnakeGame.Utils;
using System;

namespace SnakeGame.Screen
{
    class CountDownScene : OverlayScreen
    {
        private Text countDownText;
        private readonly int durationInSeconds = 3;
        private Clock clock;

        public CountDownScene(GameEngine engine) : base(engine)
        {
        }

        public override void Init()
        {
            base.Init();
            overlay.FillColor = Color.Transparent;
            countDownText = new Text("", Resources.arial)
            {
                FillColor = Color.White,
                CharacterSize = 64,
                Position = new Vector2f(engine.GetWindow().Size.X / 2, engine.GetWindow().Size.Y / 2),
            };
            clock = new Clock();
        }

        public override void ProcessEvent(Event ev)
        {
        }

        public override void Render(RenderTarget target, RenderStates states)
        {
            base.Render(target, states);
            target.Draw(countDownText);
        }

        public override void Update(float dt)
        {
            var elapsedSeconds = clock.ElapsedTime.AsSeconds();
            if (elapsedSeconds >= durationInSeconds) engine.GetMachine().PopState();
            countDownText.DisplayedString = Math.Ceiling((durationInSeconds - elapsedSeconds)).ToString();
            countDownText.Position = new Vector2f(engine.GetWindow().Size.X / 2 - countDownText.GetLocalBounds().Width / 2, engine.GetWindow().Size.Y / 2 - countDownText.GetLocalBounds().Height / 2);
        }
    }
}
