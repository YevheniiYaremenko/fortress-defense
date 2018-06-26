namespace Game.UI
{
    public class WinScreen : Screen
    {
        public void Init(
            System.Action onRestart, 
            System.Action onMenu, 
            System.Action onNext, 
            System.Action onFacebook, 
            System.Action onGoogle)
        {
            this.onRestart = onRestart;
            this.onMenu = onMenu;
            this.onNext = onNext;
            this.onFacebook = onFacebook;
            this.onGoogle = onGoogle;
        }

        System.Action onRestart;
        public void Restart() => onRestart?.Invoke();

        System.Action onMenu;
        public void Menu() => onMenu?.Invoke();

        System.Action onNext;
        public void NextLevel() => onNext?.Invoke();

        System.Action onFacebook;
        public void FacebookShare() => onFacebook?.Invoke();

        System.Action onGoogle;
        public void GoogleShare() => onGoogle?.Invoke();
    }
}