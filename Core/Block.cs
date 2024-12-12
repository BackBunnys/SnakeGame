using SFML.Graphics;
using SFML.System;

namespace SnakeGame.Core
{
    class Block : IComponent
    {
        public Vector2f Position { get; set; }
        public Vector2f Size { get; set; }
        public Vector2f TileSize { get; set; }
        public Sprite Sprite { get; set; }

        public Block(Sprite sprite, Vector2f tileSize)
        {
            Sprite = sprite;
            TileSize = tileSize;
            Init();
        }

        private void Init()
        {
            Sprite.Scale = new Vector2f(TileSize.X / Sprite.GetLocalBounds().Width, TileSize.Y / Sprite.GetLocalBounds().Height);
        }

        public IntRect GetLocalBounds()
        {
            return new IntRect(new Vector2i((int)Position.X, (int)Position.Y), new Vector2i((int)Size.X, (int)Size.Y));
        }

        public void Render(RenderTarget target, RenderStates states)
        {
            target.Draw(Sprite, states);
        }

        public void Update(float dt)
        {
            Sprite.Position = new Vector2f(Position.X * TileSize.X, Position.Y * TileSize.Y);
        }
    }
}
