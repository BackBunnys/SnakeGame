using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SnakeGame.Core;
using SnakeGame.Core.Player;
using SnakeGame.Engine;
using System.Collections.Generic;
using System.Linq;

namespace SnakeGame.Screen
{
    class RoundOverScreen : OverlayScreen
    {
        public class RoundResult
        {
            public List<Player> Players { get; set; }

            public RoundResult(List<Player> players)
            {
                Players = players;
            }
        }

        private readonly GameScreen game;
        private readonly RoundResult result;
        Text roundOverText;
        Text resultText;
        readonly int roundOverDurationInSeconds = 3;
        Clock clock;

        public RoundOverScreen(GameEngine engine, GameScreen game, RoundResult result) : base(engine)
        {
            this.result = result;
            this.game = game;
        }

        public override void Init()
        {
            base.Init();
            clock = new Clock();

            Font font = new Font(Resource.arial);
            roundOverText = new Text("ROUND OVER", font)
            {
                FillColor = Color.White,
                CharacterSize = 32,
                Style = Text.Styles.Bold
            };
            roundOverText.Position = new Vector2f(engine.GetWindow().Size.X / 2 - roundOverText.GetLocalBounds().Width / 2, engine.GetWindow().Size.Y / 6);

            resultText = new Text(GetResultText(), font)
            {
                FillColor = Color.Yellow,
                OutlineColor = Color.Black,
                OutlineThickness = 2,
                CharacterSize = 24,
                Style = Text.Styles.Bold
            };
            resultText.Position = new Vector2f(engine.GetWindow().Size.X / 2 - resultText.GetLocalBounds().Width / 2, engine.GetWindow().Size.Y / 6 * 2);

        }

        private string GetResultText()
        {
            List<Snake> snakes = result.Players.Select(player => player.Snake).ToList();

            if (snakes.TrueForAll(snake => snake.Dead))
            {
                return "Draw!";
            }

            if (snakes.TrueForAll(snake => !snake.Dead))
            {
                if (snakes.TrueForAll(snake => snake.Eated == snakes[0].Eated))
                {
                    return "Draw !";
                }
                var (maxScore, index) = snakes.Select((snake, i) => (snake.Eated, i)).Max();
                return result.Players[index].Name + " won the round!";
            }

            return result.Players.Find(player => !player.Snake.Dead).Name + " won the round!";
        }

        public override void ProcessEvent(Event ev)
        {
            
        }

        public override void Update(float dt)
        {
            if (clock.ElapsedTime.AsSeconds() > roundOverDurationInSeconds)
            {
                game.NewRound();
                engine.GetMachine().PopState();
            }
        }


        public override void Render(RenderTarget target, RenderStates states)
        {
            base.Render(target, states);
            target.Draw(roundOverText);
            target.Draw(resultText);
        }
    }
}
