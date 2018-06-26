namespace Game.UI
{
    public class ShopScreen : Screen
    {
        public void Init(System.Action onClose)
        {
            this.onClose = onClose;
        }

        System.Action onClose;
        public void Close() => onClose?.Invoke();
    }
}