using SFML.Graphics;

namespace SnakeGame.Utils
{
    static class Resources
    {
        public static Font arial { get; } = new Font("./Resources/PressStart2P-Regular.ttf"); //FIXME Resource.arial is corrupted when Properties.Settings are loaded
        public static Texture background { get; } = new Texture(ImageUtils.BitmapToByteArray(Resource.background));
        public static Texture simple_background { get; } = new Texture(ImageUtils.BitmapToByteArray(Resource.simple_background));
        public static Texture snake_tileset { get; } = new Texture(ImageUtils.BitmapToByteArray(Resource.snake_tileset));
        public static Texture block { get; } = new Texture(ImageUtils.BitmapToByteArray(Resource.block));
    }
}
