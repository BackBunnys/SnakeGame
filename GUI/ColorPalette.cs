using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakeGame.GUI
{
    class ColorPalette : IGUIComponent
    {
        public Vector2f Position { get; set; }
        public Vector2f Size { get; set; }
        public List<Color> AvailableColors { get; set; }
        public Vector2f ColorTileSize { get; set; }
        public uint ColorTileMargin { get; set; }

        private List<RectangleShape> colorShapes = new List<RectangleShape>();

        public ValueBinding<Color> Bindings { get; }

        public ColorPalette(List<Color> colors, ValueBinding<Color> bindings) 
        {
            AvailableColors = colors;
            Bindings = bindings;
        }

        public void ProcessEvent(Event ev)
        {
            if (ev.Type == EventType.MouseButtonReleased)
            {
                var mousePosition = new Vector2f(ev.MouseButton.X, ev.MouseButton.Y);
                if (new FloatRect(Position, Size).Contains(mousePosition))
                {
                    var colorShape = colorShapes.Find(shape => shape.GetGlobalBounds().Contains(mousePosition));
                    if (colorShape != null && colorShape.FillColor != Bindings.Value)
                    {
                        Bindings.Value = colorShape.FillColor;
                    }
                }
            }
        }

        public void Render(RenderTarget target, RenderStates states)
        {
            colorShapes.ForEach(shape => target.Draw(shape, states));
        }

        public void Update(float dt)
        {
            colorShapes = new List<RectangleShape>();
            var currentPosition = Position;
            var selectedColor = Bindings.Value;

            foreach (Color color in AvailableColors)
            {
                var colorShape = new RectangleShape
                {
                    Position = currentPosition,
                    FillColor = color,
                    Size = ColorTileSize,
                    OutlineThickness = color == selectedColor ? 5 : 0,
                    OutlineColor = new Color(0, 0, 0, 100)
                };
                colorShapes.Add(colorShape);
                currentPosition.X += ColorTileSize.X + ColorTileMargin;
                if (currentPosition.X + ColorTileSize.X > Position.X + Size.X)
                {
                    currentPosition.X = Position.X;
                    currentPosition.Y += ColorTileSize.Y + ColorTileMargin;
                }
            }
        }
    }
}
