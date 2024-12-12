using SFML.Window;

namespace SnakeGame
{
    static class Program
    {
        static void Main()
        {
            Application app = new Application(new VideoMode(900, 600));
            app.Run();
        }
    }
}
