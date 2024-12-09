using SFML.Graphics;

namespace SnakeGame.Core
{
    interface IComponent
    {
        void Update(float dt);
        void Render(RenderTarget target, RenderStates states);
    }
}
