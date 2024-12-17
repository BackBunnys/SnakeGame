using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;

namespace SnakeGame.GUI
{
    abstract class GUIFactory
    {
        private static GUIFactory _instance;
        public static void Init(GUIFactory factory)
        {
            _instance = factory;
        }

        public static GUIFactory Instance => _instance;
        public abstract Text Text(string content);
        public abstract Button Button(string text, Action onClick);
        public abstract LayoutContainer Container();
        public abstract Input Input(ValueBinding<string> binding);
        public abstract ColorPalette ColorPalette(List<Color> colors, ValueBinding<Color> binding);
        public abstract BindingButton BindingButton(ValueBinding<Keyboard.Key> binding);
        public abstract Segmented Segmented(Vector2f size, List<Button> options, int selectedIndex);
    }
}
