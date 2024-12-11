
using SFML.System;
using System;
using System.Collections.Generic;

namespace SnakeGame.Core.Controller
{
    class SnakeBotController
    {
        public enum Difficulty
        {
            EASY,
            NORMAL,
            HARD
        }
        public Snake Snake { get; }
        public Fruit Target { get; set; }
        public List<Block> Blocks { get; set; }
        public Difficulty Strength { get; set; } = Difficulty.NORMAL;

        public SnakeBotController(Snake snake, List<Block> blocks)
        {
            Snake = snake;
            Blocks = blocks;
        }

        public void Update()
        {
            Snake.Movement = MoveSpeed.SLOW;
            if (Target == null) return;
            UpdateSnakeMovement();
            UpdateSnakeDirection();
        }

        private void UpdateSnakeMovement()
        {
            switch (Strength)
            {
                case Difficulty.EASY:
                    Snake.Movement = MoveSpeed.SLOW;
                    break;
                case Difficulty.NORMAL:
                    Snake.Movement = MoveSpeed.NORMAL;
                    break;
                case Difficulty.HARD:
                    Snake.Movement = MoveSpeed.FAST;
                    break;
                default:
                    Snake.Movement = MoveSpeed.NORMAL;
                    break;
            }
        }

        private void UpdateSnakeDirection()
        {
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

            if (WillCollideBlocks(newDirection))
            {
                foreach (MoveDirection direction in Enum.GetValues(typeof(MoveDirection)))
                {
                    if (direction.IsOpposite(Snake.Direction)) continue;

                    if (!WillCollideBlocks(direction))
                    {
                        newDirection = direction;
                        break;
                    }
                }
            }

            if (WillCollideYourself(newDirection))
            {
                newDirection = GetTaleDirection(Snake.Position + newDirection.GetVector());
            }

            Snake.Direction = newDirection;
        }

        private bool WillCollideBlocks(MoveDirection direction)
        {
            return Blocks.Exists(block => block.Position == Snake.Position + direction.GetVector());
        }

        private bool WillCollideYourself(MoveDirection direction)
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
