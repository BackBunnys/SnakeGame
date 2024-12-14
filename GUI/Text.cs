using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace SnakeGame.GUI
{
    class Text : IGUIComponent
    {
        public Color FillColor { get => text.FillColor; set => text.FillColor = value; }
        public uint CharacterSize { get => text.CharacterSize; set => text.CharacterSize = value; }
        public SFML.Graphics.Text.Styles Style { get => text.Style; set => text.Style = value; }
        public Vector2f Position { get => text.Position; set => text.Position = value; }
        public Vector2f Size { get => text.GetGlobalBounds().Size; set => text.Scale = new Vector2f(value.X / text.GetGlobalBounds().Size.X, value.Y / text.GetGlobalBounds().Size.Y); }

        private SFML.Graphics.Text text;

        public Text(string content, Font font)
        {
            text = new SFML.Graphics.Text(content, font);
        }

        public void ProcessEvent(Event ev)
        {
            //Do not handles events
        }

        public void Render(RenderTarget target, RenderStates states)
        {
            target.Draw(text, states);
        }

        public void Update(float dt)
        {
            //Do not updates
        }

        public FloatRect GetGlobalBounds()
        {
            return text.GetGlobalBounds();
        }
    }
}
