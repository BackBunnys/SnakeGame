using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;
using System;
using static SnakeGame.Core.Tileset.SnakeTileset;

namespace SnakeGame.Core
{
    public class Snake
    {
        

        readonly Utils.Tileset tileset;
        List<Vector2f> segments;

        public List<Vector2f> Segments { get => segments; }
        public uint Eated { get => (uint) Segments.Count - 2; }
        public MoveDirection Direction { get => _direction; set => pendingDirection = value; }
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

        public Snake(Utils.Tileset tileset, Vector2u segmentSize)
        {
            this.tileset = tileset;
            SegmentSize = segmentSize;
            Reset(new Vector2i(1, 1));
        }

        public void Reset(Vector2i position)
        {
            Dead = false;
            Array values = Enum.GetValues(typeof(MoveDirection));
            _direction = (MoveDirection) values.GetValue(random.Next(values.Length));
            segments = new List<Vector2f> { new Vector2f(position.X, position.Y) };
            segments.Add(segments[0] - Direction.GetVector());
            pendingDirection = _direction;
        }

        public void Update(float dt)
        {
            if (Dead)
            {
                return;
            }

            time += dt;
            if (time < delay / Movement.GetFactor())
            {
                return;
            }

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
                Tile tile = Tile.HEAD_UP;

                if (i == 0)
                {
                    switch (Direction)
                    {
                        case MoveDirection.LEFT:
                            tile = Tile.HEAD_LEFT;
                            break;
                        case MoveDirection.RIGHT:
                            tile = Tile.HEAD_RIGHT;
                            break;
                        case MoveDirection.UP:
                            tile = Tile.HEAD_UP;
                            break;
                        case MoveDirection.DOWN:
                            tile = Tile.HEAD_DOWN;
                            break;
                        default:
                            tile = Tile.HEAD_DOWN;
                            break;
                    }
                }
                else if (i == segments.Count - 1)
                {
                    if (segments[i].Y < segments[i - 1].Y)
                    {
                        tile = Tile.TALE_DOWN;
                    }
                    if (segments[i].Y > segments[i - 1].Y)
                    {
                        tile = Tile.TALE_UP;
                    }
                    if (segments[i].X < segments[i - 1].X)
                    {
                        tile = Tile.TALE_RIGHT;
                    }
                    if (segments[i].X > segments[i - 1].X)
                    {
                        tile = Tile.TALE_LEFT;
                    }
                }
                else
                {
                    if (segments[i].Y > segments[i - 1].Y)
                    {
                        if (segments[i].X < segments[i + 1].X)
                        {
                            tile = Tile.DOWN_RIGHT;
                        }
                        else if (segments[i].X > segments[i + 1].X)
                        {
                            tile = Tile.DOWN_LEFT;
                        }
                        else
                        {
                            tile = Tile.UP_DOWN;
                        }
                    }
                    if (segments[i].Y < segments[i - 1].Y)
                    {
                        if (segments[i].X < segments[i + 1].X)
                        {
                            tile = Tile.UP_RIGHT;
                        }
                        else if (segments[i].X > segments[i + 1].X)
                        {
                            tile = Tile.RIGHT_DOWN;
                        }
                        else
                        {
                            tile = Tile.UP_DOWN;
                        }
                    }
                    if (segments[i].X > segments[i - 1].X)
                    {
                        if (segments[i].Y < segments[i + 1].Y)
                        {
                            tile = Tile.RIGHT_DOWN;
                        }
                        else if (segments[i].Y > segments[i + 1].Y)
                        {
                            tile = Tile.DOWN_LEFT;
                        }
                        else
                        {
                            tile = Tile.LEFT_RIGHT;
                        }
                    }
                    if (segments[i].X < segments[i - 1].X)
                    {
                        if (segments[i].Y < segments[i + 1].Y)
                        {
                            tile = Tile.UP_RIGHT;
                        }
                        else if (segments[i].Y > segments[i + 1].Y)
                        {
                            tile = Tile.DOWN_RIGHT;
                        }
                        else
                        {
                            tile = Tile.LEFT_RIGHT;
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
