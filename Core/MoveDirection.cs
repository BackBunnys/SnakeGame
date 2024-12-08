using SFML.System;

namespace SnakeGame.Core
{
    public enum MoveDirection
    {
        UP,
        DOWN,
        RIGHT,
        LEFT
    };

    public static class MoveDirectionExtensions
    {
        public static Vector2f GetVector(this MoveDirection direction)
        {
            switch (direction)
            {
                case MoveDirection.UP: return new Vector2f(0, -1);
                case MoveDirection.DOWN: return new Vector2f(0, 1);
                case MoveDirection.LEFT: return new Vector2f(-1, 0);
                case MoveDirection.RIGHT: return new Vector2f(1, 0);
                default: return new Vector2f();
            }
        }

        public static bool IsOpposite(this MoveDirection direction, MoveDirection another)
        {
            return (direction.GetVector() + another.GetVector()) == new Vector2f(0, 0); 
        }
    }
}
