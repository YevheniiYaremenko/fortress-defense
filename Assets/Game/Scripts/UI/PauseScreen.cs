namespace Game.UI
{
    public class PauseScreen : Screen
    {
        public void Init(System.Action onRestart, System.Action onMenu, System.Action onClose)
        {
            this.onRestart = onRestart;
            this.onMenu = onMenu;
            this.onClose = onClose;
        }

        System.Action onRestart;
        public void Restart() => onRestart?.Invoke();

        System.Action onMenu;
        public void Menu() => onMenu?.Invoke();

        System.Action onClose;
        public void Close() => onClose?.Invoke();
    }
}