
using SFML.System;
using System;

namespace SnakeGame.Core.Controller
{
    class SnakeBotController
    {
        public Snake Snake { get; }
        public Fruit Target { get; set; }

        public SnakeBotController(Snake snake)
        {
            Snake = snake;
        }

        public void Update()
        {
            Snake.Movement = MoveSpeed.SLOW;
            if (Target == null) return;
            Snake.Movement = MoveSpeed.FAST;

            MoveDirection newDirection = Snake.Direction;

            if (Target.X > Snake.Position.X) newDirection = MoveDirection.RIGHT;
            else if (Target.X < Snake.Position.X) newDirection = MoveDirection.LEFT;
            else if (Target.Y > Snake.Position.Y) newDirection = MoveDirection.DOWN;
            else if (Target.Y < Snake.Position.Y) newDirection = MoveDirection.UP;

            if (newDirection.IsOpposite(Snake.Direction))
            {
                var vector = newDirection.GetVector();
                newDirection = MoveDirectionExtensions.FromVector(new Vector2f(vector.Y, vector.X));
            }

            if (WillCollide(newDirection))
            {
                newDirection = GetTaleDirection(Snake.Position + newDirection.GetVector());
            }

            Snake.Direction = newDirection;
        }

        private bool WillCollide(MoveDirection direction)
        {
            return Snake.Segments.Contains(Snake.Position + direction.GetVector());
        }

        public MoveDirection GetTaleDirection(Vector2f segment)
        {
            int segmentIndex = Snake.Segments.IndexOf(segment);
            Vector2f prevSegment = Snake.Segments[segmentIndex - 1];
            return MoveDirectionExtensions.FromVector(new Vector2f(segment.X - prevSegment.X, segment.Y - prevSegment.Y));
        }


    }
}
