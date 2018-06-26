using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameController : Singleton<GameController>
    {
        [Header("UI")]
        [SerializeField] UI.GameScreen gameScreen;
        [SerializeField] UI.PauseScreen pauseScreen;
        [SerializeField] UI.SettingsScreen settingsScreen;
        [SerializeField] UI.WinScreen winScreen;
        [SerializeField] UI.LoseScreen loseScreen;
        [SerializeField] UI.HelpScreen helpScreen;
        [SerializeField] UI.ShopScreen shopScreen;
        List<UI.Screen> screens;
        Stack<UI.Screen> screenHistory;
        UI.Screen currentScreen;

        private void Awake()
        {
            screens = new List<UI.Screen>()
            {
                gameScreen,
                pauseScreen,
                settingsScreen,
                winScreen,
                loseScreen,
                helpScreen,
                shopScreen,
            };
            screenHistory = new Stack<UI.Screen>();
        }

        private void Start()
        {
            gameScreen.Init(
                () => ShowScreen(pauseScreen),
                () => ShowScreen(settingsScreen),
                () => ShowScreen(helpScreen),
                () => ShowScreen(shopScreen)
                );
            pauseScreen.Init(LoadGame, LoadMenu, BackScreen);
            settingsScreen.Init(BackScreen);
            winScreen.Init(LoadGame, LoadMenu, null, null, null);
            loseScreen.Init(LoadGame, LoadMenu);
            helpScreen.Init(BackScreen);
            shopScreen.Init(BackScreen);

            ShowScreen(gameScreen);
        }

        #region Scene Navigation

        void LoadMenu() => SceneNavigationManager.Instance.LoadMenu();

        void LoadGame() => SceneNavigationManager.Instance.LoadGame();

        #endregion

        #region UI

        void ShowScreen(UI.Screen screen)
        {
            screens.ForEach(s => s.Hide());
            screen.Show();
            
            while (screenHistory.Contains(screen))
            {
                screenHistory.Pop();
            }
            currentScreen = screen;
            screenHistory.Push(currentScreen);
        }

        void BackScreen()
        {
            if (screenHistory.Count > 1)
            {
                screenHistory.Pop();
                ShowScreen(screenHistory.Pop());
            }
        }

        #endregion
    }
}
