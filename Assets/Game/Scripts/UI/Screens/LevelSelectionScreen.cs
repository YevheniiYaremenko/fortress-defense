namespace Game.UI
{
    public class LevelSelectionScreen : Screen
    {
        public void Init(System.Action onBack, System.Action onSelectLevel)
        {
            this.onBack = onBack;
            this.onSelectLevel = onSelectLevel;
        }

        System.Action onBack;
        public void Back() => onBack?.Invoke();

        System.Action onSelectLevel;
        public void SelectLevel() => onSelectLevel?.Invoke();
    }
}