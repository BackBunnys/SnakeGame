using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakeGame.Utils
{
    public class Tileset
    {
        readonly Sprite[,] spriteMap;
        List<string> bindings;

        public Tileset(Texture texture) : this(texture, 64) { }

        public Tileset(Texture texture, int tilesize)
        {
            Sprite[,] sprites = new Sprite[texture.Size.Y / tilesize, texture.Size.X / tilesize];
            Vector2i tileSize = new Vector2i(tilesize, tilesize);
            for (int i = 0; i < texture.Size.Y / tilesize; i++)
            {
                for (int j = 0; j < texture.Size.X / tilesize; j++)
                {
                    Vector2i tilePosition = new Vector2i(j * tilesize, i * tilesize);
                    Sprite tileSprite = new Sprite(texture, new IntRect(tilePosition, tileSize));
                    sprites[i, j] = tileSprite;
                }
            }
            spriteMap = sprites;
        }

        public void Bind(List<string> names)
        {
            bindings = names;
        }

        public void Bind<T>(List<T> names) where T : Enum {
            bindings = new List<string>();
            names.ForEach(name => bindings.Add(name.ToString()));
        }

        public Sprite GetTile(uint x, uint y)
        {
            return new Sprite(spriteMap[y, x]);
        }

        public Sprite GetTile(string name)
        {
            int position = bindings.IndexOf(name);
            int row = position / spriteMap.GetLength(1);
            int column = position % spriteMap.GetLength(1);
            return new Sprite(spriteMap[row, column]);
        }

        public Sprite GetTile<T>(T name) where T : Enum
        {
            return GetTile(name.ToString());
        }

        public List<Sprite> GetTiles()
        {
            return spriteMap.Cast<Sprite>().ToList();
        }
    }
}
