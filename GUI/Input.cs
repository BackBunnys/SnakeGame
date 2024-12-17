using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace SnakeGame.GUI
{
    class Input : IGUIComponent
    {
        public Text Text { get; set; }
        public string Content { get => Text.Content; set => Text.Content = value; }
        public Vector2f Position { get => Text.Position; set => Text.Position = value; }
        public Vector2f Size { get => Text.Size; set => Text.Size = value; }
        public bool Focused { get; set; }
        public ValueBinding<string> Bindings { get; }

        public Input(Text text, ValueBinding<string> bindings)
        {
            Text = text;
            Bindings = bindings;
        }

        public Color Color = Color.White;
        public Color FocusColor = Color.White;

        public void ProcessEvent(Event ev)
        {
            if (ev.Type == EventType.MouseButtonReleased)
            {
                Focused = Text.GetGlobalBounds().Contains(new Vector2f(ev.MouseButton.X, ev.MouseButton.Y));
            }
            if (Focused)
            {
                if (ev.Type == EventType.TextEntered)
                {
                    if (ev.Text.Unicode <= 'z' && ev.Text.Unicode >= ' ')
                    {
                        Bindings.Value = Content + (char) ev.Text.Unicode;
                    }
                }
                if (ev.Type == EventType.KeyPressed)
                {
                    if (ev.Key.Code == Keyboard.Key.Backspace)
                    {
                        if (Text.Content.Length > 0)
                        {
                            Bindings.Value = Text.Content.Substring(0, Text.Content.Length - 1);
                        }
                    }
                }
            }
        }

        public void Render(RenderTarget target, RenderStates states)
        {
            Text.Render(target, states);
        }

        public void Update(float dt)
        {
            Text.FillColor = Focused ? FocusColor : Color;
            Content = Bindings.Value;
        }
    }
}
