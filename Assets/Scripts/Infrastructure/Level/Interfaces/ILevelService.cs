namespace Infrastructure.Level.Interfaces
{
    public interface ILevelService
    {
        void LevelStart();
        void PauseGame();
        void PlayGame();
        void OnAttack();
    }
}