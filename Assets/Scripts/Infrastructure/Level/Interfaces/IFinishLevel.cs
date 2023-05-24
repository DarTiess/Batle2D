namespace Infrastructure.Level
{
    public interface IFinishLevel
    {
        void LevelLost();
        void LevelWin();
    }
}