using SFML.Graphics;
using System.Collections.Generic;

namespace SnakeGame.Core.Tileset
{
    class SnakeTileset : Utils.Tileset
    {
        public enum Tile
        {
            UP_RIGHT,
            LEFT_RIGHT,
            RIGHT_DOWN,
            UP_DOWN,
            DOWN_LEFT,
            DOWN_RIGHT,
            HEAD_UP,
            HEAD_DOWN,
            HEAD_LEFT,
            HEAD_RIGHT,
            TALE_UP,
            TALE_LEFT,
            TALE_RIGHT,
            TALE_DOWN,
            EMPTY,
            FRUIT,
        }

        public static List<Tile> Bindings { get; } = new List<Tile>()
        {
            Tile.UP_RIGHT,
            Tile.LEFT_RIGHT,
            Tile.RIGHT_DOWN,
            Tile.HEAD_UP,
            Tile.HEAD_RIGHT,
            Tile.DOWN_RIGHT,
            Tile.EMPTY,
            Tile.UP_DOWN,
            Tile.HEAD_LEFT,
            Tile.HEAD_DOWN,
            Tile.EMPTY,
            Tile.EMPTY,
            Tile.DOWN_LEFT,
            Tile.TALE_UP,
            Tile.TALE_RIGHT,
            Tile.FRUIT,
            Tile.EMPTY,
            Tile.EMPTY,
            Tile.TALE_LEFT,
            Tile.TALE_DOWN
        };

        public SnakeTileset(Texture texture, int tilesize = 64) : base(texture, tilesize)
        {
            Bind(Bindings);
        }
    }
}
