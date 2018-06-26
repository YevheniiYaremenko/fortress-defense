namespace Game.UI
{
    public class MenuScreen : Screen
    {
        public void Init(System.Action onExitGame, System.Action onMoreGames, System.Action onSelectLevel)
        {
            this.onExitGame = onExitGame;
            this.onSelectLevel = onSelectLevel;
        }

        System.Action onExitGame;
        public void ExitGame() => onExitGame?.Invoke();

        System.Action onMoreGames;
        public void ShowMoreGames() => onMoreGames?.Invoke();

        System.Action onSelectLevel;
        public void SelectLevel() => onSelectLevel?.Invoke();
    }
}