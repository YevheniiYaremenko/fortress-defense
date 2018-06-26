using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class GameScreen : Screen
    {
        [SerializeField] Image healthBar;
        [SerializeField] Text timeText;
        [SerializeField] Text scoreText;
        [SerializeField] Text killsText;
        [SerializeField] Text coinsText;

        public void Init(
            System.Action onPause,
            System.Action onSettings,
            System.Action onUnitsShop,
            System.Action onHelp,
            System.Action onShop)
        {
            this.onPause = onPause;
            this.onSettings = onSettings;
            this.onHelp = onHelp;
            this.onShop = onShop;
            this.onUnitsShop = onUnitsShop;
        }

        public void SetData(float healthValue, int score, float time, int kills, int coins)
        {
            healthBar.fillAmount = healthValue;
            scoreText.text = score.ToString();
            timeText.text = TimeHelper.GetTime((int)time);
            killsText.text = kills.ToString();
            coinsText.text = coins.ToString();
        }

        System.Action onPause;
        public void Pause() => onPause?.Invoke();

        System.Action onSettings;
        public void Settings() => onSettings?.Invoke();

        System.Action onHelp;
        public void Help() => onHelp?.Invoke();

        System.Action onShop;
        public void Shop() => onShop?.Invoke();

        System.Action onUnitsShop;
        public void UnitsShop() => onUnitsShop?.Invoke();
    }
}