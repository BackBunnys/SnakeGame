using SFML.System;

namespace SnakeGame.GUI
{
    class LayoutContainer : Container
    {
        public enum DirectionType
        {
            HORIZONTAL,
            VERTICAL
        }

        public enum AlignType
        {
            START,
            MIDDLE,
            END
        }

        public DirectionType Direction { get; set; } = DirectionType.VERTICAL;
        public AlignType Align { get; set; } = AlignType.MIDDLE;

        public void ReplaceComponents()
        {
            float alignFactor = GetAlignFactor();
            Vector2f directionVector = GetDirectionVector();
            Vector2f componentsGap = CalculateGap();
            Vector2f lastComponentEnd = Position;
            for (int i = 0; i < Components.Count; ++i)
            {
                var newPosition = lastComponentEnd;
                if (i != 0)
                {
                    newPosition += componentsGap;
                }
                newPosition = new Vector2f(newPosition.X * directionVector.X, newPosition.Y * directionVector.Y); //Resetting alignment
                newPosition += new Vector2f(Position.X * directionVector.Y, Position.Y * directionVector.X); //Back to position
                newPosition += new Vector2f(Size.X * alignFactor * directionVector.Y, Size.Y * alignFactor * directionVector.X); //Applying container alignment
                newPosition -= new Vector2f(Components[i].Size.X * alignFactor * directionVector.Y, Components[i].Size.Y * alignFactor * directionVector.X); //Applying component alignment
                Components[i].Position = newPosition;
                lastComponentEnd = newPosition + Components[i].Size;
            }
        }

        private Vector2f CalculateGap()
        {
            Vector2f contentSize = new Vector2f(0, 0);
            Components.ForEach(component => contentSize += component.Size);
            return (Size - contentSize) / (Components.Count - 1); //Gaps count is less then elements count
        }

        private float GetAlignFactor()
        {
            switch (Align)
            {
                case AlignType.START: return 0;
                case AlignType.MIDDLE: return 0.5f;
                case AlignType.END: return 1;
                default: return 0;
            }
        }

        private Vector2f GetDirectionVector()
        {
            switch (Direction)
            {
                case DirectionType.HORIZONTAL: return new Vector2f(1, 0);
                case DirectionType.VERTICAL: return new Vector2f(0, 1);
                default: return new Vector2f(0, 0);
            }
        }

        public override void Update(float dt)
        {
            ReplaceComponents(); //TODO replace
            Components.ForEach(component => component.Update(dt));
        }
    }
}
