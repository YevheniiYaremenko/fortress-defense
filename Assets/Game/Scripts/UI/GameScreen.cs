using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class GameScreen : Screen
    {
        [SerializeField] Image healthBar;
        [SerializeField] Image scoreBar;
        [SerializeField] Image enemiesBar;

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

        public void SetData(float healthValue, float scoreValue, float enemyValue)
        {
            healthBar.fillAmount = healthValue;
            scoreBar.fillAmount = scoreValue;
            enemiesBar.fillAmount = enemyValue;
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