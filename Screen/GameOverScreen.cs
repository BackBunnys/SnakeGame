using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SnakeGame.Core;
using SnakeGame.Engine;
using SnakeGame.Utils;
using System.Collections.Generic;
using System.Linq;

namespace SnakeGame.Screen
{
    class GameOverScreen : OverlayScreen
    {
        public class GameResult
        {
            public Dictionary<Snake, uint> ScoreMap { get; set; }

            public GameResult(Dictionary<Snake, uint> scoreMap)
            {
                ScoreMap = scoreMap;
            }
        }

        Text gameOverText;
        Text restartText;
        Text exitText;
        Text resultText;
        readonly GameResult result;
        readonly GameSetup setup;

        public GameOverScreen(GameEngine engine, GameResult result, GameSetup setup) : base(engine)
        {
            this.result = result;
            this.setup = setup;
        }

        public override void Init()
        {
            base.Init();
            Font font = new Font(Resource.arial);
            gameOverText = new Text("GAME OVER", font)
            {
                FillColor = Color.White,
                CharacterSize = 32,
                Style = Text.Styles.Bold
            };
            gameOverText.Position = new Vector2f(engine.GetWindow().Size.X / 2 - gameOverText.GetLocalBounds().Width / 2, engine.GetWindow().Size.Y / 6);

            resultText = new Text(GetResultText(), font)
            {
                FillColor = Color.Yellow,
                OutlineColor = Color.Black,
                OutlineThickness = 2,
                CharacterSize = 24,
                Style = Text.Styles.Bold
            };
            resultText.Position = new Vector2f(engine.GetWindow().Size.X / 2 - resultText.GetLocalBounds().Width / 2, engine.GetWindow().Size.Y / 6 * 2);

            restartText = new Text("Restart", font)
            {
                FillColor = Color.White,
                CharacterSize = 24,
                Style = Text.Styles.Bold
            };
            restartText.Position = new Vector2f(engine.GetWindow().Size.X / 2 - restartText.GetLocalBounds().Width / 2, engine.GetWindow().Size.Y / 6 * 3);
            exitText = new Text("Exit", font)
            {
                FillColor = Color.White,
                CharacterSize = 24,
                Style = Text.Styles.Bold
            };
            exitText.Position = new Vector2f(engine.GetWindow().Size.X / 2 - exitText.GetLocalBounds().Width / 2, engine.GetWindow().Size.Y / 6 * 4);
        }

        private string GetResultText()
        {
            if (result.ScoreMap.Count == 1) //Signleplayer
            {
                Snake snake = new List<Snake>(result.ScoreMap.Keys)[0];
                if (snake.Dead) return "You lost!";
                else return "You won!";
            }

            //Multiplayer
            List<uint> values = new List<uint>(result.ScoreMap.Values);
            
            if (values.TrueForAll(value => value == values[0]))
            {
                return "Draw!";
            }
            var (maxValue, index) = values.Select((value, i) => (value, i)).Max();
            return result.ScoreMap.ElementAt(index).Key.Name + " won the game!";
        }

        public override void ProcessEvent(Event ev)
        {
            if (ev.Type == EventType.MouseButtonReleased && ev.MouseButton.Button == Mouse.Button.Left)
            {
                if (restartText.GetGlobalBounds().Inflate(new Vector2f(50, 50)).Contains(ev.MouseButton.X, ev.MouseButton.Y))
                {
                    engine.GetMachine().Clear();
                    engine.GetMachine().PushState(new GameScreen(engine, setup));
                }
                if (exitText.GetGlobalBounds().Inflate(new Vector2f(50, 50)).Contains(ev.MouseButton.X, ev.MouseButton.Y))
                {
                    engine.GetMachine().Clear();
                }
            }
            if (ev.Type == EventType.MouseMoved)
            {
                restartText.FillColor = Color.White;
                exitText.FillColor = Color.White;
                if (restartText.GetGlobalBounds().Inflate(new Vector2f(50, 50)).Contains(ev.MouseMove.X, ev.MouseMove.Y))
                {
                    restartText.FillColor = Color.Yellow;
                }
                if (exitText.GetGlobalBounds().Inflate(new Vector2f(50, 50)).Contains(ev.MouseMove.X, ev.MouseMove.Y))
                {
                    exitText.FillColor = Color.Yellow;
                }
            }
        }

        public override void Render(RenderTarget target, RenderStates states)
        {
            base.Render(target, states);
            target.Draw(gameOverText, states);
            target.Draw(resultText, states);
            target.Draw(restartText, states);
            target.Draw(exitText, states);
        }

        public override void Update(float dt)
        {
           
        }
    }
}
