using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;
using SnakeGame.Utils;
using System;

namespace SnakeGame.Core
{
    public class Snake
    {
        public enum SnakeTileset
        {
            UP_RIGHT,
            LEFT_RIGHT,
            RIGHT_DOWN,
            UP_DOWN,
            DOWN_LEFT,
            DOWN_RIGHT,
            HEAD_UP,
            HEAD_DOWN,
            HEAD_LEFT,
            HEAD_RIGHT,
            TALE_UP,
            TALE_LEFT,
            TALE_RIGHT,
            TALE_DOWN,
            EMPTY,
            FRUIT,
        }

        public static List<SnakeTileset> Bindings { get; } = new List<SnakeTileset>()
        {
            SnakeTileset.UP_RIGHT,
            SnakeTileset.LEFT_RIGHT,
            SnakeTileset.RIGHT_DOWN,
            SnakeTileset.HEAD_UP,
            SnakeTileset.HEAD_RIGHT,
            SnakeTileset.DOWN_RIGHT,
            SnakeTileset.EMPTY,
            SnakeTileset.UP_DOWN,
            SnakeTileset.HEAD_LEFT,
            SnakeTileset.HEAD_DOWN,
            SnakeTileset.EMPTY,
            SnakeTileset.EMPTY,
            SnakeTileset.DOWN_LEFT,
            SnakeTileset.TALE_UP,
            SnakeTileset.TALE_RIGHT,
            SnakeTileset.FRUIT,
            SnakeTileset.EMPTY,
            SnakeTileset.EMPTY,
            SnakeTileset.TALE_LEFT,
            SnakeTileset.TALE_DOWN
        };

        readonly Tileset tileset;
        List<Vector2f> segments;

        public List<Vector2f> Segments { get => segments; }
        public MoveDirection Direction { get => _direction; set => pendingDirection = value; }
        public string Name { get; set; }
        public Color Color { get; set; } = Color.Green;

        public MoveSpeed Movement { get; set; } = MoveSpeed.NORMAL;
        public Vector2u SegmentSize { get; set; }

        public float Speed { get => 1 / delay; set => delay = 1 / value; }
        public Vector2f Position { get => segments[0]; set => segments[0] = value; }

        public bool Dead { get; set; } = false;

        float time = 0;
        private float delay = 100;
        private MoveDirection _direction;
        private MoveDirection pendingDirection;
        private static readonly Random random = new Random();

        public Snake(Tileset tileset, Vector2u segmentSize)
        {
            this.tileset = tileset;
            SegmentSize = segmentSize;
            Reset();
        }

        public void Reset()
        {
            Dead = false;
            Array values = Enum.GetValues(typeof(MoveDirection));
            _direction = (MoveDirection) values.GetValue(random.Next(values.Length));
            segments = new List<Vector2f>
            {
                new Vector2f(random.Next(5, 15), random.Next(5, 15))
            };
            segments.Add(segments[0] - Direction.GetVector());
            pendingDirection = _direction;
        }

        public void Update(float dt)
        {
            if (Dead) return;

            time += dt;
            if (time < delay / Movement.GetFactor()) return;

            time = 0;
            _direction = pendingDirection;

            Move();
        }

        public void Move()
        {
            for (int i = segments.Count - 1; i > 0; --i)
            {
                segments[i] = segments[i - 1];
            }
            segments[0] += _direction.GetVector();
        }

        public void GrowUp()
        {
            Vector2f talePosition = segments[segments.Count - 1];
            Vector2f nextTaleMove = segments[segments.Count - 2] - segments[segments.Count - 1];
            this.segments.Add(talePosition - nextTaleMove);
        }

        public void Render(RenderTarget target, RenderStates states)
        {
            for (int i = 0; i < segments.Count; i++)
            {
                SnakeTileset tile = SnakeTileset.HEAD_UP;

                if (i == 0)
                {
                    switch (Direction)
                    {
                        case MoveDirection.LEFT:
                            tile = SnakeTileset.HEAD_LEFT;
                            break;
                        case MoveDirection.RIGHT:
                            tile = SnakeTileset.HEAD_RIGHT;
                            break;
                        case MoveDirection.UP:
                            tile = SnakeTileset.HEAD_UP;
                            break;
                        case MoveDirection.DOWN:
                            tile = SnakeTileset.HEAD_DOWN;
                            break;
                    }
                }
                else if (i == segments.Count - 1)
                {
                    if (segments[i].Y < segments[i - 1].Y)
                    {
                        tile = SnakeTileset.TALE_DOWN;
                    }
                    if (segments[i].Y > segments[i - 1].Y)
                    {
                        tile = SnakeTileset.TALE_UP;
                    }
                    if (segments[i].X < segments[i - 1].X)
                    {
                        tile = SnakeTileset.TALE_RIGHT;
                    }
                    if (segments[i].X > segments[i - 1].X)
                    {
                        tile = SnakeTileset.TALE_LEFT;
                    }
                }
                else
                {
                    if (segments[i].Y > segments[i - 1].Y)
                    {
                        if (segments[i].X < segments[i + 1].X)
                        {
                            tile = SnakeTileset.DOWN_RIGHT;
                        }
                        else if (segments[i].X > segments[i + 1].X)
                        {
                            tile = SnakeTileset.DOWN_LEFT;
                        }
                        else
                        {
                            tile = SnakeTileset.UP_DOWN;
                        }
                    }
                    if (segments[i].Y < segments[i - 1].Y)
                    {
                        if (segments[i].X < segments[i + 1].X)
                        {
                            tile = SnakeTileset.UP_RIGHT;
                        }
                        else if (segments[i].X > segments[i + 1].X)
                        {
                            tile = SnakeTileset.RIGHT_DOWN;
                        }
                        else
                        {
                            tile = SnakeTileset.UP_DOWN;
                        }
                    }
                    if (segments[i].X > segments[i - 1].X)
                    {
                        if (segments[i].Y < segments[i + 1].Y)
                        {
                            tile = SnakeTileset.RIGHT_DOWN;
                        }
                        else if (segments[i].Y > segments[i + 1].Y)
                        {
                            tile = SnakeTileset.DOWN_LEFT;
                        }
                        else
                        {
                            tile = SnakeTileset.LEFT_RIGHT;
                        }
                    }
                    if (segments[i].X < segments[i - 1].X)
                    {
                        if (segments[i].Y < segments[i + 1].Y)
                        {
                            tile = SnakeTileset.UP_RIGHT;
                        }
                        else if (segments[i].Y > segments[i + 1].Y)
                        {
                            tile = SnakeTileset.DOWN_RIGHT;
                        }
                        else
                        {
                            tile = SnakeTileset.LEFT_RIGHT;
                        }
                    }
                }

                Sprite snakeSprite = tileset.GetTile(tile);
                if (!Dead)
                {
                    snakeSprite.Color = Color;
                }
                snakeSprite.Position = new Vector2f(segments[i].X * SegmentSize.X, segments[i].Y * SegmentSize.Y);
                snakeSprite.Scale = new Vector2f(SegmentSize.X / snakeSprite.GetLocalBounds().Width, SegmentSize.Y / snakeSprite.GetLocalBounds().Height);
                target.Draw(snakeSprite, states);
            }
        }
    }
}
