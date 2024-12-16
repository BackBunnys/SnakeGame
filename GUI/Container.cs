using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;

namespace SnakeGame.GUI
{
    abstract class Container : IGUIComponent
    {
        public Vector2f Position { get; set; }
        public Vector2f Size { get; set; }

        public List<IGUIComponent> Components { get; } = new List<IGUIComponent>();

        public virtual void ProcessEvent(Event ev)
        {
            Components.ForEach(component => component.ProcessEvent(ev));
        }

        public virtual void Render(RenderTarget target, RenderStates states)
        {
            Components.ForEach(component => component.Render(target, states));
        }

        public virtual void Update(float dt)
        {
            Components.ForEach(component => component.Update(dt));
        }
    }
}
