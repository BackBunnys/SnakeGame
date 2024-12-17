using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;

namespace SnakeGame.GUI
{
    class Segmented : IGUIComponent
    {
        public Vector2f Position { get => layoutContainer.Position; set => layoutContainer.Position = value; }
        public Vector2f Size { get => layoutContainer.Size; set => layoutContainer.Size = value; }

        private LayoutContainer layoutContainer;
        public int SelectedOptionIndex { get; set; }

        private List<Button> Options { get; set; }

        private Color currentOptionColor;

        public Segmented(Vector2f size, List<Button> options, int selectedIndex)
        {
            Options = options;
            SelectedOptionIndex = selectedIndex;
            layoutContainer = new LayoutContainer();
            Size = size;
            layoutContainer.Direction = LayoutContainer.DirectionType.HORIZONTAL;

            for (int i = 0; i < options.Count; ++i)
            {
                int localI = i;
                options[i].OnClick += () => OnSelect(localI);
            }
            layoutContainer.Components.AddRange(options);

            currentOptionColor = Options[SelectedOptionIndex].Color;
            OnSelect(SelectedOptionIndex);
        }

        public void OnSelect(int index)
        {
            Options[SelectedOptionIndex].Color = currentOptionColor;
            SelectedOptionIndex = index;
            currentOptionColor = Options[SelectedOptionIndex].Color;
            Options[SelectedOptionIndex].Color = Options[SelectedOptionIndex].HoverColor;
        }

        public void ProcessEvent(Event ev)
        {
            layoutContainer.ProcessEvent(ev);
        }

        public void Render(RenderTarget target, RenderStates states)
        {
            layoutContainer.Render(target, states);
        }

        public void Update(float dt)
        {
            layoutContainer.Update(dt);
        }
    }
}
