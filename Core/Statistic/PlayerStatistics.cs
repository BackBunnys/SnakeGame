using SFML.Graphics;
using SFML.System;
using SnakeGame.Core.Tileset;
using SnakeGame.Utils;

namespace SnakeGame.Core.Statistic
{
    class PlayerStatistics : IComponent
    {
        private Sprite snakePreview;
        private Sprite fruitPreview;
        private Text playerName;
        private Text playerEated;

        public Player.Player Player { get; set; }
        public Vector2f Position { get; set; }

        public PlayerStatistics(Player.Player player, Utils.Tileset tileset)
        {
            Player = player;
            Init(tileset);
        }

        protected void Init(Utils.Tileset tileset)
        {
            snakePreview = tileset.GetTile(SnakeTileset.Tile.HEAD_DOWN);
            snakePreview.Scale = new Vector2f(0.65f, 0.65f);
            snakePreview.Color = Player.Color;

            Font font = Resources.arial;
            playerName = new Text(Player.Name, font)
            {
                Position = new Vector2f(snakePreview.Position.X + snakePreview.GetGlobalBounds().Width + 15, 0),
                CharacterSize = 22,
                OutlineColor = Color.Black,
                OutlineThickness = 1
            };

            playerEated = new Text(Player.Snake.Eated.ToString(), font)
            {
                Position = new Vector2f(snakePreview.Position.X + snakePreview.GetGlobalBounds().Width + 15, 25),
                CharacterSize = 22,
                OutlineColor = Color.Black,
                OutlineThickness = 1
            };

            fruitPreview = tileset.GetTile(SnakeTileset.Tile.FRUIT);
            fruitPreview.Color = new Color(255, 100, 100);
            fruitPreview.Scale = new Vector2f(0.35f, 0.35f);
            fruitPreview.Position = new Vector2f(playerEated.Position.X + playerEated.GetGlobalBounds().Width + 50, playerEated.Position.Y);
        }

        public void Render(RenderTarget target, RenderStates states)
        {
            states.Transform.Translate(Position);
            target.Draw(snakePreview, states);
            target.Draw(playerName, states);
            target.Draw(playerEated, states);
            target.Draw(fruitPreview, states);
        }

        public void Update(float dt)
        {
            playerName.DisplayedString = Player.Name;
            playerEated.DisplayedString = Player.Snake.Eated.ToString();
        }
    }
}
