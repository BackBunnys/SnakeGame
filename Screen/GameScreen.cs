using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SnakeGame.Core;
using SnakeGame.Core.Controller;
using SnakeGame.Engine;
using SnakeGame.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakeGame.Screen
{
    class GameScreen : IState //TODO: refactor
    {
        struct Fruct
        {
            public float x, y;
        }

        const uint N = 30, M = 20;
        uint tile_width;
        uint tile_height;

        private Fruct fruit;
        readonly GameSetup setup;
        readonly List<Snake> snakes = new List<Snake>();
        readonly List<SnakeKeyboardController> controllers = new List<SnakeKeyboardController>();
        readonly Random random = new Random();
        Sprite fruitSprite;
        Tileset tileset;
        Statistics statistics;
        Dictionary<Snake, uint> scoreMap;
        int roundNumber = 1;

        void Tick(Snake snake, ref Fruct fruit)
        {
            
            if ((snake.Position.X == fruit.x) && (snake.Position.Y == fruit.y))
            {
                snake.GrowUp();
                RespawnFruit();
            }

            if (snake.Position.X >= N || snake.Position.X < 0)
            {
                snake.Dead = true;
            }
            if  (snake.Position.Y > M - 2|| snake.Position.Y < 2)
            {
                snake.Dead = true;
            }
        }

        private bool IsRoundOver()
        {
            if (snakes.Count > 1)
            {
                if (snakes.FindAll(snake => !snake.Dead).Count <= 1)
                {
                    return true;
                }
            } else
            {
                if (snakes[0].Dead)
                {
                    return true;
                }
            }

            if (snakes.Select(snake => snake.Segments.Count).Sum() == N * M)
            {
                return true;
            }

            return false;
        }

        private void CheckCollisions()
        {
            List<Vector2f> heads = new List<Vector2f>();
            snakes.ForEach(snake => heads.Add(snake.Position));
            foreach (Snake snake in snakes)
            {
                for (int i = 0; i < heads.Count; ++i)
                {
                    if (snake.Segments.FindLastIndex(segment => segment == heads[i]) > 0)
                    {
                        snakes[i].Dead = true;
                        return;
                    }
                }
            }

        }

        public GameScreen(GameEngine appData, GameSetup gameSetup) : base(appData)
        {
            setup = gameSetup;
        }

        public override void Init()
        {
            Font font = new Font(Resource.arial);
            tile_width = engine.GetWindow().Size.X / N;
            tile_height = engine.GetWindow().Size.Y / M;

            fruit = new Fruct
            {
                x = 10,
                y = 10
            };

            tileset = Tileset.FromTexture(new Texture(ImageUtils.BitmapToByteArray(Resource.snake_tileset)));
            tileset.Bind(Snake.Bindings);

            var snake = new Snake(tileset, tile_width)
            {
                Name = "Player 1"
            };
            snakes.Add(snake);
            controllers.Add(new SnakeKeyboardController(DefaultKeyboardBindings.PLAYER_ONE, snake));

            if (setup.Type == GameSetup.GameType.MULTIPLAYER)
            {
                Snake secondSnake = new Snake(tileset, tile_width)
                {
                    Name = "Player 2",
                    Color = new Color(255, 165, 0)
                };
                snakes.Add(secondSnake);
                if (setup.VersusBot) { /*TODO*/ }
                else controllers.Add(new SnakeKeyboardController(DefaultKeyboardBindings.PLAYER_TWO, secondSnake));
            }


            fruitSprite = tileset.GetTile(Snake.SnakeTileset.FRUIT);
            fruitSprite.Color = new Color(255, 100, 100);
            fruitSprite.Scale = new Vector2f(tile_width / fruitSprite.GetLocalBounds().Width, tile_height / fruitSprite.GetLocalBounds().Height);

            statistics = new Statistics(snakes, tileset); 
            statistics.Position = new Vector2f(engine.GetWindow().Size.X / 2 - statistics.Size.X / 2, 0);


            scoreMap = new Dictionary<Snake, uint>();
            snakes.ForEach(snakeItem => scoreMap.Add(snakeItem, 0));
        }

        public void NewRound()
        {
            snakes.ForEach(snake => snake.Reset());
            RespawnFruit();
            roundNumber++;
        }

        private void RespawnFruit()
        {
            fruit.x = random.Next() % N;
            fruit.y = random.Next() % (M - 4) + 3;
        }

        public override void ProcessEvent(Event ev)
        {
            if (ev.Type == EventType.KeyPressed)
            {
                controllers.ForEach(controller => controller.OnKeyPressed(ev.Key.Code));
                if (ev.Key.Code == Keyboard.Key.Escape) engine.GetMachine().PushState(new PauseScreen(engine, setup));
            }
            if (ev.Type == EventType.KeyReleased)
            {
                controllers.ForEach(controller => controller.OnKeyReleased(ev.Key.Code));
            }
        }

        public override void Render(RenderTarget target, RenderStates states)
        {
            snakes.ForEach(snake => snake.Render(target, states));
            statistics.Render(target, states);
            fruitSprite.Position = new Vector2f(fruit.x * tile_width, fruit.y * tile_height);
            target.Draw(fruitSprite, states);
        }

        public override void Update(float dt)
        {
            snakes.ForEach(snake =>
            {
                snake.Update(dt);
                Tick(snake, ref fruit);
            });
            CheckCollisions();
            if (IsRoundOver())
            {
                UpdateScoreMap();

                if (IsGameOver())
                {
                    engine.GetMachine().PushState(new GameOverScreen(engine, new GameOverScreen.GameResult(scoreMap), setup));
                } 
                else
                {
                    engine.GetMachine().PushState(new RoundOverScreen(engine, this, new RoundOverScreen.RoundResult(scoreMap)));
                }
            }
        }

        private bool IsGameOver()
        {
            List<uint> scores = new List<uint>(scoreMap.Values);
            uint maxScore = scores.Max();
            return maxScore >= (setup.RoundCount + 1) / 2 || roundNumber >= setup.RoundCount;
        }

        private void UpdateScoreMap()
        {
            List<Snake> aliveSnakes = snakes.FindAll(snake => !snake.Dead);
            if (aliveSnakes.Count > 0)
            {
                var (maxScore, index) = aliveSnakes.Select((snake, i) => (snake.Segments.Count - 2, i)).Max();
                scoreMap[aliveSnakes[index]]++;
            }
        }
    }
}
