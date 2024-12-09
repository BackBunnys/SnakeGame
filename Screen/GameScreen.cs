using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SnakeGame.Core;
using SnakeGame.Core.Controller;
using SnakeGame.Core.Player;
using SnakeGame.Core.Statistic;
using SnakeGame.Core.Tileset;
using SnakeGame.Engine;
using SnakeGame.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakeGame.Screen
{
    class GameScreen : IState //TODO: refactor
    {
        uint N = 34, M = 20;
        uint tile_width;
        uint tile_height;

        private Fruit fruit;
        readonly GameSetup setup;
        readonly List<Player> players = new List<Player>();
        static readonly Random random = new Random();
        private Tileset tileset;
        private Statistics statistics;
        int roundNumber = 0;
        readonly uint fieldOffset = 65;

        void Tick(Snake snake)
        {

            if ((snake.Position.X == fruit.X) && (snake.Position.Y == fruit.Y))
            {
                snake.GrowUp();
                RespawnFruit();
            }

            if (snake.Position.X >= N || snake.Position.X < 0)
            {
                snake.Dead = true;
            }
            if (snake.Position.Y >= M || snake.Position.Y < 0)
            {
                snake.Dead = true;
            }
        }

        private bool IsRoundOver()
        {
            if (players.Count > 1)
            {
                if (players.FindAll(player => !player.Snake.Dead).Count <= 1)
                {
                    return true;
                }
            }
            else
            {
                if (players[0].Snake.Dead)
                {
                    return true;
                }
            }

            if (players.Select(player => player.Snake.Segments.Count).Sum() == N * M)
            {
                return true;
            }

            return false;
        }

        private void CheckCollisions()
        {
            List<Vector2f> heads = new List<Vector2f>();
            players.ForEach(player => heads.Add(player.Snake.Position));
            foreach (Player player in players)
            {
                for (int i = 0; i < heads.Count; ++i)
                {
                    var headPosition = player.Snake.Segments.FindLastIndex(segment => segment == heads[i]);
                    if (headPosition > 0)
                    {
                        players[i].Snake.Dead = true;
                        return;
                    }
                    else if (headPosition == 0)
                    {
                        if (player.Snake != players[i].Snake) //Head collision with another snake
                        {
                            if (players[i] is BotPlayer && player is BotPlayer) //Both are bots
                            {
                                players[i].Snake.Dead = true;
                                player.Snake.Dead = true;
                                return;
                            }

                            if (!(players[i] is BotPlayer)) //first are not bot
                                players[i].Snake.Dead = true;
                            if (!(player is BotPlayer)) //second are not bot
                                player.Snake.Dead = true;
                        }
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
            SetupTileset();
            SetupField();
            SetupFruit();
            SetupSnakes();
            SetupStatistics();

            NewRound();
        }

        private void SetupTileset()
        {
            tileset = new SnakeTileset(Resources.snake_tileset);
        }

        private void SetupField()
        {
            N = setup.FieldSize.X;
            M = setup.FieldSize.Y;
            tile_width = engine.GetWindow().Size.X / N;
            tile_height = (engine.GetWindow().Size.Y - fieldOffset) / M;
        }

        private void SetupFruit()
        {
            var fruitSprite = tileset.GetTile(SnakeTileset.Tile.FRUIT);
            fruitSprite.Color = new Color(255, 100, 100);
            fruit = new Fruit(fruitSprite, new Vector2u(tile_width, tile_height));
        }

        private void SetupSnakes()
        {
            Snake snake = new Snake(tileset, new Vector2u(tile_width, tile_height));
            players.Add(new HumanPlayer(snake, new SnakeKeyboardController(DefaultKeyboardBindings.PLAYER_ONE, snake)) { Name = "Player 1" });

            if (setup.Type == GameSetup.GameType.MULTIPLAYER)
            {
                Snake secondSnake = new Snake(tileset, new Vector2u(tile_width, tile_height));
                if (setup.VersusBot)
                    players.Add(new BotPlayer(secondSnake, new SnakeBotController(secondSnake) { Target = fruit }) { Name = "Bot", Color = Color.Yellow });
                else 
                    players.Add(new HumanPlayer(secondSnake, new SnakeKeyboardController(DefaultKeyboardBindings.PLAYER_TWO, secondSnake)) { Name = "Player 2", Color = new Color(255, 165, 0) });
            }
        }

        private void SetupStatistics()
        {
            statistics = new Statistics(players, tileset)
            {
                Size = new Vector2f(engine.GetWindow().Size.X, 60)
            };
            statistics.Position = new Vector2f(engine.GetWindow().Size.X / 2 - statistics.Size.X / 2, 0);
            statistics.RoundCount = setup.RoundCount;
        }

        public void NewRound()
        {
            players.ForEach(player => player.Snake.Reset());
            RespawnFruit();
            roundNumber++;
            statistics.Round = roundNumber;
        }

        private void RespawnFruit()
        {
            fruit.X = random.Next() % N;
            fruit.Y = random.Next() % M;
        }

        public override void ProcessEvent(Event ev)
        {
            if (ev.Type == EventType.KeyPressed && ev.Key.Code == Keyboard.Key.Escape) engine.GetMachine().PushState(new PauseScreen(engine, setup));
            players.ForEach(player => player.ProcessEvent(ev));
        }

        public override void Render(RenderTarget target, RenderStates states)
        {
            statistics.Render(target, states);
            states.Transform.Translate(0, fieldOffset);
            players.ForEach(player => player.Render(target, states));
            fruit.Render(target, states);
        }

        public override void Update(float dt)
        {
            players.ForEach(player =>
            {
                player.Update(dt);
                Tick(player.Snake);
            });
            fruit.Update(dt);
            CheckCollisions();
            statistics.Update(dt);
            if (IsRoundOver())
            {
                UpdateScores();
                statistics.Update(dt);

                if (IsGameOver())
                {
                    engine.GetMachine().PushState(new GameOverScreen(engine, new GameOverScreen.GameResult(players), setup));
                }
                else
                {
                    engine.GetMachine().PushState(new RoundOverScreen(engine, this, new RoundOverScreen.RoundResult(players)));
                }
            }
        }

        private bool IsGameOver()
        {
            if (setup.RoundCount == null) return false;

            List<uint> scores = players.Select(player => player.Score).ToList();
            uint maxScore = scores.Max();
            return maxScore >= setup.RoundCount / 2 + 1 || roundNumber >= setup.RoundCount;
        }

        private void UpdateScores()
        {
            List<Player> alivePlayers = players.FindAll(player => !player.Snake.Dead);
            if (alivePlayers.Count > 0)
            {
                var (maxScore, index) = alivePlayers.Select((player, i) => (player.Snake.Eated, i)).Max();
                alivePlayers[index].Score++;
            }
        }
    }
}
