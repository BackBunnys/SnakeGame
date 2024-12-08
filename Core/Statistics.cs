using SFML.Graphics;
using SFML.System;
using SnakeGame.Utils;
using System.Collections.Generic;

namespace SnakeGame.Core
{
    class Statistics
    {
        private readonly List<Snake> snakes;
        private RectangleShape statisticsBox;
        private Sprite snakePreviewSprite;
        private Sprite fruitSprite;
        private Text text;
        private readonly Tileset tileset;

        public Vector2f Position { get => statisticsBox.Position;  set => statisticsBox.Position = value; }
        public Vector2f Size { get => statisticsBox.Size; set => statisticsBox.Size = value; }

        public Statistics(List<Snake> snakes, Tileset tileset)
        {
            this.snakes = snakes;
            this.tileset = tileset;

            Init();
        }

        private void Init()
        {
            Font font = new Font(Resource.arial);

            statisticsBox = new RectangleShape(new Vector2f(300, 60))
            {
                FillColor = new Color(0, 0, 0, 100)
            };
            text = new Text("text", font)
            {
                FillColor = Color.White
            };
            snakePreviewSprite = tileset.GetTile(Snake.SnakeTileset.HEAD_DOWN);
            snakePreviewSprite.Scale = new Vector2f(0.75f, 0.75f);

            fruitSprite = tileset.GetTile(Snake.SnakeTileset.FRUIT);
            fruitSprite.Color = new Color(255, 100, 100);
            fruitSprite.Scale = new Vector2f(0.5f, 0.5f);
        }

        public void Render(RenderTarget target, RenderStates states)
        {
            target.Draw(statisticsBox);

            for (int i = 0; i < snakes.Count; ++i)
            {
                Snake snake = snakes[i];
                snakePreviewSprite.Color = snake.Color;
                var offset = statisticsBox.GetGlobalBounds().Width / snakes.Count * i;
                snakePreviewSprite.Position = statisticsBox.Position + new Vector2f(offset, statisticsBox.Size.Y / 2 - snakePreviewSprite.GetGlobalBounds().Height / 2);
                target.Draw(snakePreviewSprite, states);
                text.DisplayedString = (snake.Segments.Count - 2).ToString();
                text.Position = new Vector2f(snakePreviewSprite.Position.X + snakePreviewSprite.GetGlobalBounds().Width + 20, statisticsBox.Size.Y / 2 - text.GetGlobalBounds().Height);
                target.Draw(text, states);
                Sprite fruitPreview = new Sprite(fruitSprite)
                {
                    Position = new Vector2f(text.Position.X + text.GetGlobalBounds().Width + 20, text.Position.Y)
                };
                target.Draw(fruitPreview, states);
            }
        }
    }
}
