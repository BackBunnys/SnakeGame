using SFML.System;
using System;
using System.Collections.Generic;
using static SnakeGame.GUI.GUIBuilder;
using static SnakeGame.GUI.LayoutContainer;

namespace SnakeGame.GUI
{
    class GUIBuilder : IGUIBuilder
    {
        public interface IGUIBuilder
        {
            GUIFactory Factory { get; }
            List<IGUIComponent> Components { get; }
        }

        public class GUIContainerBuilder<T> : IGUIBuilder where T : IGUIBuilder
        {
            public LayoutContainer LayoutContainer { get; private set; }
            public T Builder {get; private set;}

            public GUIFactory Factory => Builder.Factory;

            public List<IGUIComponent> Components => LayoutContainer.Components;

            public GUIContainerBuilder(T builder, LayoutContainer container)
            {
                Builder = builder;
                LayoutContainer = container;
            }

            public GUIContainerBuilder<GUIContainerBuilder<T>> Column(Vector2f size, AlignType align = AlignType.MIDDLE)
            {
                return Container(size, DirectionType.VERTICAL, align);
            }

            public GUIContainerBuilder<GUIContainerBuilder<T>> Row(Vector2f size, AlignType align = AlignType.MIDDLE)
            {
                return Container(size, DirectionType.HORIZONTAL, align);
            }

            public GUIContainerBuilder<GUIContainerBuilder<T>> Column(Func<Vector2f, Vector2f> sizeFunc, AlignType align = AlignType.MIDDLE)
            {
                return Container(sizeFunc.Invoke(LayoutContainer.Size), DirectionType.VERTICAL, align);
            }

            public GUIContainerBuilder<GUIContainerBuilder<T>> Row(Func<Vector2f, Vector2f> sizeFunc, AlignType align = AlignType.MIDDLE) //Relative row size
            {
                return Container(sizeFunc.Invoke(LayoutContainer.Size), DirectionType.HORIZONTAL, align);
            }

            private GUIContainerBuilder<GUIContainerBuilder<T>> Container(Vector2f size, DirectionType direction, AlignType align = AlignType.MIDDLE)
            {
                LayoutContainer container = Factory.Container();
                container.Size = size;
                container.Direction = direction;
                container.Align = align;
                Components.Add(container);
                return new GUIContainerBuilder<GUIContainerBuilder<T>>(this, container);
            }

            public GUIContainerBuilder<T> Component(IGUIComponent component)
            {
                Components.Add(component);
                return this;
            }

            public T Close()
            {
                return Builder;
            }
        }

        public GUIFactory Factory { get; private set; }
        public List<IGUIComponent> Components { get; } = new List<IGUIComponent>();

        public GUIBuilder(GUIFactory factory)
        {
            Factory = factory;
        }

        public GUIContainerBuilder<GUIBuilder> Column(Vector2f size, AlignType align = AlignType.MIDDLE)
        {
            return Container(size, DirectionType.VERTICAL, align);
        }

        public GUIContainerBuilder<GUIBuilder> Row(Vector2f size, AlignType align = AlignType.MIDDLE)
        {
            return Container(size, DirectionType.HORIZONTAL, align);
        }

        private GUIContainerBuilder<GUIBuilder> Container(Vector2f size, DirectionType direction, AlignType align = AlignType.MIDDLE)
        {
            LayoutContainer container = Factory.Container();
            container.Size = size;
            container.Direction = direction;
            container.Align = align;
            Components.Add(container);
            return new GUIContainerBuilder<GUIBuilder>(this, container);
        }

        public GUIBuilder Component(IGUIComponent component)
        {
            Components.Add(component);
            return this;
        }

        public IGUIComponent Build()
        {
            return Components[0]; //TODO FIXME
        }
    }
}
