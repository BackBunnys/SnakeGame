using SFML.Graphics;
using SFML.System;
using SnakeGame.Core.Tileset;
using SnakeGame.Core;
using System.Collections.Generic;

namespace SnakeGame.Core
{
    class Statistics
    {
        private readonly List<Player.Player> players;
        private RectangleShape statisticsBox;
        private Sprite snakePreviewSprite;
        private Sprite fruitSprite;
        private Text text;
        private readonly Utils.Tileset tileset;

        public Vector2f Position { get => statisticsBox.Position;  set => statisticsBox.Position = value; }
        public Vector2f Size { get => statisticsBox.Size; set => statisticsBox.Size = value; }
        public int Round { get; set; }
        public uint? RoundCount { get; set; }

        public Statistics(List<Player.Player> players, Utils.Tileset tileset)
        {
            this.players = players;
            this.tileset = tileset;

            Init();
        }

        private void Init()
        {
            Font font = new Font(Resource.arial);

            statisticsBox = new RectangleShape(new Vector2f(400, 60))
            {
                FillColor = new Color(0, 0, 0, 100),
            };
            text = new Text("text", font)
            {
                FillColor = Color.White,
                OutlineColor = Color.Black,
                OutlineThickness = 1
            };
            snakePreviewSprite = tileset.GetTile(SnakeTileset.Tile.HEAD_DOWN);
            snakePreviewSprite.Scale = new Vector2f(0.65f, 0.65f);

            fruitSprite = tileset.GetTile(SnakeTileset.Tile.FRUIT);
            fruitSprite.Color = new Color(255, 100, 100);
            fruitSprite.Scale = new Vector2f(0.5f, 0.5f);
        }

        public void Render(RenderTarget target, RenderStates states)
        {
            target.Draw(statisticsBox);

            text.CharacterSize = 26;
            if (RoundCount != 1) //Not a fast game
            {
                text.DisplayedString = Round + (RoundCount != null ? " / " + RoundCount : "");
                text.Position = statisticsBox.Position + new Vector2f(statisticsBox.GetGlobalBounds().Width / 2 - text.GetGlobalBounds().Width / 2, 0);
                target.Draw(text);
                if (players.Count > 1)
                {
                    text.DisplayedString = players[0].Score + " : " + players[1].Score; //TODO Support for more snakes?
                    text.Position = statisticsBox.Position + new Vector2f(statisticsBox.GetGlobalBounds().Width / 2 - text.GetGlobalBounds().Width / 2, 25);
                    text.FillColor = Color.Yellow;
                    target.Draw(text);
                }
                text.FillColor = Color.White;
            }
            text.CharacterSize = 24;

            for (int i = 0; i < players.Count; ++i)
            {
                Snake snake = players[i].Snake;
                snakePreviewSprite.Color = snake.Color;
                var offset = statisticsBox.GetGlobalBounds().Width / (players.Count) * i + (i * 50);
                snakePreviewSprite.Position = statisticsBox.Position + new Vector2f(offset, statisticsBox.Size.Y / 2 - snakePreviewSprite.GetGlobalBounds().Height / 2);
                target.Draw(snakePreviewSprite, states);
                text.DisplayedString = players[i].Name;
                text.Position = new Vector2f(snakePreviewSprite.Position.X + snakePreviewSprite.GetGlobalBounds().Width + 15, 0);
                target.Draw(text);
                text.DisplayedString = snake.Eated.ToString();
                text.Position = new Vector2f(snakePreviewSprite.Position.X + snakePreviewSprite.GetGlobalBounds().Width + 15, 25);
                target.Draw(text, states);
                Sprite fruitPreview = new Sprite(fruitSprite)
                {
                    Position = new Vector2f(text.Position.X + text.GetGlobalBounds().Width + 15, text.Position.Y)
                };
                target.Draw(fruitPreview, states);
            }
        }
    }
}
