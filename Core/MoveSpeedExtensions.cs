
namespace SnakeGame.Core
{
    public enum MoveSpeed
    {
        NORMAL,
        SLOW,
        FAST
    }

    static class MoveSpeedExtensions
    {
        public static float GetFactor(this MoveSpeed speed)
        {
            switch (speed)
            {
                case MoveSpeed.FAST:
                    return 1.5f;
                case MoveSpeed.SLOW:
                    return 0.5f;
                case MoveSpeed.NORMAL:
                default: return 1f;
            }
        }
    }
}
