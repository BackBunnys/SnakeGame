using SFML.System;
using SnakeGame.Core;

namespace SnakeGame.GUI
{
    interface IGUIComponent : IInteractableComponent
    {
        Vector2f Position { get; set; }
        Vector2f Size { get; set; }
    }
}
