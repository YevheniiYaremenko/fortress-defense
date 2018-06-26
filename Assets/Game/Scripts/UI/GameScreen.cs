namespace Game.UI
{
    public class GameScreen : Screen
    {
        public void Init(
            System.Action onPause,
            System.Action onSettings,
            System.Action onHelp,
            System.Action onShop)
        {
            this.onPause = onPause;
            this.onSettings = onSettings;
            this.onHelp = onHelp;
            this.onShop = onShop;
        }

        System.Action onPause;
        public void Pause() => onPause?.Invoke();

        System.Action onSettings;
        public void Settings() => onSettings?.Invoke();

        System.Action onHelp;
        public void Help() => onHelp?.Invoke();

        System.Action onShop;
        public void Shop() => onShop?.Invoke();
    }
}