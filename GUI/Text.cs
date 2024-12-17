using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace SnakeGame.GUI
{
    class Text : IGUIComponent
    {
        public Color FillColor { get => text.FillColor; set => text.FillColor = value; }
        public uint CharacterSize { get => text.CharacterSize; set => text.CharacterSize = value; }
        public SFML.Graphics.Text.Styles Style { get => text.Style; set => text.Style = value; }
        public Vector2f Position { get => text.Position; set => text.Position = value; }
        public Vector2f Size { get => text.GetGlobalBounds().Size; set { text.Scale = new Vector2f(1, 1); text.Scale = new Vector2f(value.X / text.GetGlobalBounds().Size.X, value.Y / text.GetGlobalBounds().Size.Y); } }
        public string Content { get => text.DisplayedString; set => text.DisplayedString = value; }
        public float OutlineThickness { get => text.OutlineThickness; set => text.OutlineThickness = value; }
        public Color OutlineColor { get => text.OutlineColor; set => text.OutlineColor = value; }

        private readonly SFML.Graphics.Text text;
        private readonly Func<string> contentFunc;

        public Text(Func<string> contentFunc, Font font)
        {
            this.contentFunc = contentFunc;
            text = new SFML.Graphics.Text(contentFunc.Invoke(), font);
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
            text.DisplayedString = contentFunc.Invoke();
        }

        public FloatRect GetGlobalBounds()
        {
            return text.GetGlobalBounds();
        }
    }
}
