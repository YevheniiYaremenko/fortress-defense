namespace Game.UI
{
    public class LoseScreen : Screen
    {
        public void Init(System.Action onRestart, System.Action onMenu)
        {
            this.onRestart = onRestart;
            this.onMenu = onMenu;
        }

        System.Action onRestart;
        public void Restart() => onRestart?.Invoke();

        System.Action onMenu;
        public void Menu() => onMenu?.Invoke();
    }
}