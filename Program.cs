using SFML.Window;

namespace SnakeGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Application app = new Application(new VideoMode(800, 600));
            app.Run();
        }
    }
}
