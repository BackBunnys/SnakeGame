using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;

namespace SnakeGame.Core
{
    class Spawner 
    {
        public IntRect Bounds { get; set; }
        private static readonly Random random = new Random();

        public Spawner(IntRect bounds)
        {
            Bounds = bounds;
        }

        public Vector2i GetSpawnPosition(List<Vector2i> restrictedPositions)
        {
            Vector2i randomPosition;
            do
            {
                randomPosition = new Vector2i(random.Next(Bounds.Left, Bounds.Left + Bounds.Width), random.Next(Bounds.Top, Bounds.Top + Bounds.Height));
            } while (restrictedPositions.Contains(randomPosition));

            return randomPosition;
        }
    }
}
