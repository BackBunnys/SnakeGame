using SFML.Window;

namespace SnakeGame.Core
{
    interface IInteractableComponent : IComponent
    {
        void ProcessEvent(Event ev);
    }
}
