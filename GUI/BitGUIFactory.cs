using SFML.Graphics;
using SnakeGame.Utils;
using System;

namespace SnakeGame.GUI
{
    class BitGUIFactory : GUIFactory
    {
        public Font Font { get; set; }
        public BitGUIFactory()
        {
            Font = Resources.arial;
        }

        public override Button Button(string text, Action onClick)
        {
            Button button = new Button(Text(text));
            button.OnClick += onClick;
            return button;
        }

        public override Text Text(string content)
        {
            return new Text(content, Font)
            {
                FillColor = Color.White,
                CharacterSize = 24,
                Style = SFML.Graphics.Text.Styles.Bold
            };
        }

        public override Container Container()
        {
            return new Container();
        }
    }
}
