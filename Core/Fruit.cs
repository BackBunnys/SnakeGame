using SFML.Graphics;
using SFML.System;

namespace SnakeGame.Core
{
    class Fruit
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Sprite Sprite { get; set; }
        public Vector2u TileSize { get; set; }

        public Fruit(Sprite sprite, Vector2u tilesize)
        {
            Sprite = sprite;
            TileSize = tilesize;
            sprite.Scale = new Vector2f(tilesize.X / sprite.GetLocalBounds().Width, tilesize.Y / sprite.GetLocalBounds().Height);

        }

        public virtual void Update(float dt)
        {
            Sprite.Position = new Vector2f(X * TileSize.X, Y * TileSize.Y);
        }

        public virtual void Render(RenderTarget target, RenderStates states)
        {
            target.Draw(Sprite, states);
        }
    }
}
