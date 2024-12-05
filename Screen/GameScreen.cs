using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SnakeGame.Engine;
using System;
using System.IO;

namespace SnakeGame.Screen
{
    class GameScreen : IState //TODO: refactor
    {
        struct Snake
        {
            public float x, y;
        }

        struct Fruct
        {
            public float x, y;
        }

        const uint N = 40, M = 30;
        uint tile_width;
        uint tile_height;

        int direction, num = 4;
        Clock clock;
        float timer, delay;
        private Fruct fruit;
        private Snake[] snake;
        Random random = new Random();
        Sprite snakeSprite;
        Sprite fruitSprite;
        Sprite fieldSprite;


        void Tick(ref Snake[] s, ref Fruct f)
        {
            for (int i = num; i > 0; --i)
            {
                s[i].x = s[i - 1].x;
                s[i].y = s[i - 1].y;
            }

            if (direction == 0) s[0].y += 1;
            if (direction == 1) s[0].x -= 1;
            if (direction == 2) s[0].x += 1;
            if (direction == 3) s[0].y -= 1;

            if ((s[0].x == f.x) && (s[0].y == f.y))
            {
                num++;
                f.x = random.Next() % N;
                f.y = random.Next() % M;
            }

            if (s[0].x >= N) s[0].x = 0; if (s[0].x < 0) s[0].x = N;
            if (s[0].y >= M) s[0].y = 0; if (s[0].y < 0) s[0].y = M;

            for (int i = 1; i < num; i++)
                if (s[0].x == s[i].x && s[0].y == s[i].y) num = i;
        }
       

        public GameScreen(GameEngine appData) : base(appData)
        {
        }

        public override void Init()
        {
            clock = new Clock();
            timer = 0;
            delay = 0.1f;
            tile_width = engine.GetWindow().Size.X / N;
            tile_height = engine.GetWindow().Size.Y / M;

            fruit = new Fruct();
            snake = new Snake[100];
            for (int i = 0; i < snake.Length; i++)
            {
                snake[i] = new Snake();
            }

            fruit.x = 10;
            fruit.y = 10;


            Texture snakeTexture = new Texture(ImageToByte(Resource.green));
            Texture fruitTexture = new Texture(ImageToByte(Resource.red));
            Texture fieldTexture = new Texture(ImageToByte(Resource.white));
            snakeSprite = new Sprite(snakeTexture);
            fruitSprite = new Sprite(fruitTexture);
            fieldSprite = new Sprite(fieldTexture);
        }

        public static byte[] ImageToByte(System.Drawing.Bitmap img)
        {
            using (var stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }

        public override void ProcessEvent(Event ev)
        {
            
        }

        public override void Render(RenderTarget target)
        {

            for (int i = 0; i < N; i++)
                for (int j = 0; j < M; j++)
                {
                    fieldSprite.Position = new Vector2f(i * tile_width, j * tile_height);
                    target.Draw(fieldSprite);
                }

            for (int i = 0; i < num; i++)
            {
                snakeSprite.Position = new Vector2f(snake[i].x * tile_width, snake[i].y * tile_height);
                target.Draw(snakeSprite);
            }

            fruitSprite.Position = new Vector2f(fruit.x * tile_width, fruit.y * tile_height);
            target.Draw(fruitSprite);
        }

        public override void Update()
        {
            float time = clock.ElapsedTime.AsSeconds();
            clock.Restart();
            timer += time;

            if (Keyboard.IsKeyPressed(Keyboard.Key.A)) direction = 1;
            if (Keyboard.IsKeyPressed(Keyboard.Key.D)) direction = 2;
            if (Keyboard.IsKeyPressed(Keyboard.Key.W)) direction = 3;
            if (Keyboard.IsKeyPressed(Keyboard.Key.S)) direction = 0;

            if (timer > delay) { 
                timer = 0; 
                Tick(ref snake, ref fruit); 
            }
        }
    }
}
