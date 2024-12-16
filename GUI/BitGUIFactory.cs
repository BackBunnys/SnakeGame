using SFML.Graphics;
using SFML.Window;
using SnakeGame.Utils;
using System;
using System.Collections.Generic;

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
                CharacterSize = 20,
                Style = SFML.Graphics.Text.Styles.Bold,
                OutlineColor = Color.Black,
                OutlineThickness = 1
            };
        }

        public override LayoutContainer Container()
        {
            return new LayoutContainer();
        }

        public override Input Input(ValueBinding<string> binding)
        {
            return new Input(Text(binding.Value), binding)
            {
                FocusColor = Color.Yellow
            };
        }

        public override ColorPalette ColorPalette(List<Color> colors, ValueBinding<Color> binding)
        {
            return new ColorPalette(colors, binding);
        }

        public override BindingButton BindingButton(ValueBinding<Keyboard.Key> binding)
        {
            return new BindingButton(Text(binding.Value.ToString()), binding)
            {
                Size = new SFML.System.Vector2f(50, 50)
            };
        }
    }
}
