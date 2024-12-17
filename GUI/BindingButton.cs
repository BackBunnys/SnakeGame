using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace SnakeGame.GUI
{
    class BindingButton : IGUIComponent
    {
        public Vector2f Position { get; set; }
        public Vector2f Size { get; set; }

        public bool IsBinding { get; private set; }
        public Text Text { get; set; }
        public ValueBinding<Keyboard.Key> Bindings { get; private set; }

        private readonly RectangleShape buttonShape;

        public BindingButton(Text text, ValueBinding<Keyboard.Key> bindings)
        {
            Text = text;
            Bindings = bindings;
            buttonShape = new RectangleShape(Size)
            {
                FillColor = new Color(0, 0, 0, 100),
                OutlineColor = Color.Black,
                OutlineThickness = 2
            };
        }

        public void ProcessEvent(Event ev)
        {
            if (ev.Type == EventType.MouseButtonReleased)
            {
                IsBinding = buttonShape.GetGlobalBounds().Contains(new Vector2f(ev.MouseButton.X, ev.MouseButton.Y));
            }
            if (IsBinding && ev.Type == EventType.KeyReleased)
            {
                if (ev.Key.Code == Keyboard.Key.Escape)
                {
                    IsBinding = false;
                }
                else
                {
                    Bindings.Value = ev.Key.Code;
                    IsBinding = false;
                }
            }
        }

        public void Render(RenderTarget target, RenderStates states)
        {
            target.Draw(buttonShape, states);
            Text.Render(target, states);
        }

        public void Update(float dt)
        {
            buttonShape.Position = Position;
            buttonShape.Size = Size;
            Text.Content = IsBinding ? "?" : Bindings.Value.ToString();
            Text.Size = buttonShape.Size / 3 * 2;
            Text.Position = buttonShape.Position + buttonShape.Size / 2 - Text.GetGlobalBounds().Size / 2;
        }
    }
}
