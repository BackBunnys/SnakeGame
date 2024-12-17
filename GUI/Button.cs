using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SnakeGame.Core;
using SnakeGame.Utils;
using System;

namespace SnakeGame.GUI
{
    class Button : IGUIComponent
    {
        public Vector2f Position { get => Text.Position; set => Text.Position = value; }
        public Vector2f Size { get => Text.Size; set => Text.Size = value; }
        public Vector2f HoverPadding { get; set; } = new Vector2f(50, 50);
        public Color Color { get; set; } = Color.White;
        public Color HoverColor { get; set; } = Color.Yellow;

        public event Action OnClick;

        public Text Text { get; set; }
        private bool Hovered { get; set; } = false;

        public Button(Text text)
        {
            Text = text;
            Text.FillColor = Color;
        }

        public void ProcessEvent(Event ev)
        {
            if (ev.Type == EventType.MouseMoved)
            {
                Hovered = IsInBounds(new Vector2f(ev.MouseMove.X, ev.MouseMove.Y));
            }
            if (ev.Type == EventType.MouseButtonReleased)
            {
                if (Hovered) 
                {
                    OnClick?.Invoke();
                }
            }
        }

        private bool IsInBounds(Vector2f point)
        {
            return Text.GetGlobalBounds().Inflate(HoverPadding).Contains(point.X, point.Y);
        }

        public void Render(RenderTarget target, RenderStates states)
        {
            Text.Render(target, states);
        }

        public void Update(float dt)
        {
            Text.FillColor = Hovered ? HoverColor : Color;
        }
    }
}
