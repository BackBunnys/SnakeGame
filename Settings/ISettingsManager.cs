namespace SnakeGame.Settings
{
    interface ISettingsManager
    {
        Settings Get();
        void Save(Settings settings);
    }
}
