using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Utils
{
    static class SFMLExtensions
    {
        public static FloatRect Inflate(this FloatRect rect, Vector2f size)
        {
            rect.Width += size.X;
            rect.Height += size.Y;
            rect.Top -= size.Y / 2;
            rect.Left -= size.X / 2;
            return rect;
        }
    }
}
