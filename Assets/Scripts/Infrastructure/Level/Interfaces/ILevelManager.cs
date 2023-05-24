namespace Infrastructure.Level
{
    public interface ILevelManager
    {
        void LevelStart();
        void PauseGame();
        void PlayGame();
        void OnAttack();
    }
}