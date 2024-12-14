using SFML.Graphics;
using System;

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
        public abstract Container Container();
    }
}
