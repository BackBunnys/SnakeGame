using SFML.Graphics;
using SFML.System;
using SnakeGame.Utils;
using System.Collections.Generic;

namespace SnakeGame.Core.Statistic
{
    class Statistics : IComponent
    {
        private readonly List<Player.Player> players;
        private RectangleShape statisticsBox;
        private readonly List<PlayerStatistics> playerStatistics = new List<PlayerStatistics>();
        private Text roundText = null;
        private Text scoreText = null;

        public Vector2f Position { get => statisticsBox.Position;  set { statisticsBox.Position = value; ResetPositions(); } }
        public Vector2f Size { get => statisticsBox.Size; set { statisticsBox.Size = value; ResetPositions(); } }
        public int Round { get; set; }
        public uint? RoundCount { get; set; }

        public Statistics(List<Player.Player> players, Utils.Tileset tileset)
        {
            this.players = players;
            Init(players, tileset);
        }

        private void Init(List<Player.Player> players, Utils.Tileset tileset)
        {
            Font font = Resources.arial;

            statisticsBox = new RectangleShape(new Vector2f(600, 60))
            {
                FillColor = new Color(0, 0, 0, 100),
            };

            if (RoundCount != 1) //Not a fast game
            {
                roundText = new Text("1", font)
                {
                    CharacterSize = 24,
                    FillColor = Color.White,
                    OutlineColor = Color.Black,
                    OutlineThickness = 1,
                };

                if (players.Count > 1)
                {
                    scoreText = new Text(roundText)
                    {
                        FillColor = Color.Yellow
                    };
                }
            }

            players.ForEach(player => playerStatistics.Add(new PlayerStatistics(player, tileset)));
            
            ResetPositions();
        }

        private void ResetPositions()
        {
            if (roundText != null) roundText.Position = statisticsBox.Position + new Vector2f(statisticsBox.GetGlobalBounds().Width / 2 - roundText.GetGlobalBounds().Width / 2, 5);
            if (scoreText != null) scoreText.Position = statisticsBox.Position + new Vector2f(statisticsBox.GetGlobalBounds().Width / 2 - scoreText.GetGlobalBounds().Width / 2, 30);

            for (int i = 0; i < playerStatistics.Count; ++i)
            {
                var offset = statisticsBox.GetGlobalBounds().Width / 4 *(i + i * 2) + i * -20;
                playerStatistics[i].Position = statisticsBox.Position + new Vector2f(offset, 10);
            }
        }

        public void Update(float dt)
        {
            if (roundText != null)
            {
                roundText.DisplayedString = Round + (RoundCount != null ? " / " + RoundCount : "");
                if (scoreText != null)
                {
                    scoreText.DisplayedString = players[0].Score + " : " + players[1].Score; //TODO Support for more snakes?
                }
                ResetPositions();
            }

            playerStatistics.ForEach(playerStatistic => playerStatistic.Update(dt));
        }

        public void Render(RenderTarget target, RenderStates states)
        {
            target.Draw(statisticsBox, states);
            if (roundText != null) target.Draw(roundText, states);
            if (scoreText != null) target.Draw(scoreText, states);
            playerStatistics.ForEach(playerStatistic => playerStatistic.Render(target, states));
        }
    }
}
