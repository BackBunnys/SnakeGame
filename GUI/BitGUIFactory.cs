﻿using SFML.Graphics;
using SFML.System;
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

        public override Text Text(Func<string> contentFunc)
        {
            return new Text(contentFunc, Font)
            {
                FillColor = Color.White,
                CharacterSize = 22,
                Style = SFML.Graphics.Text.Styles.Bold,
                OutlineColor = new Color(100, 100, 100),
                OutlineThickness = 1
            };
        }

        public override Text Text(string content)
        {
            return Text(() => content);
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

        public override Segmented Segmented(Vector2f size, List<Button> options, int selectedIndex)
        {
            return new Segmented(size, options, selectedIndex);
        }
    }
}
