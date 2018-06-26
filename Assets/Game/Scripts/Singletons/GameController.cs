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

        [Header("Scene")]
        [SerializeField] Transform rightLevelBorder;

        [Header("Game")]
        [SerializeField] int enemiesCount = 10;
        [SerializeField] float startHealth = 1000;
        float health;
        int killedEnemies = 0;

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

            var camera = Camera.main;
            camera.transform.position = new Vector3(
                rightLevelBorder.position.x - camera.orthographicSize * camera.aspect,
                0,
                -2
            );

            StartGame();
        }

        private void Update()
        {
            gameScreen.SetData(health / startHealth, 0, 1 - killedEnemies / (float)enemiesCount);

            if (health <= 0)
            {
                Lose();
                return;
            }

            if (killedEnemies >= enemiesCount)
            {
                Win();
                return;
            }
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

        #region Game
        
        void StartGame()
        {
            health = startHealth;
            Spawner.EnemyZoneSpawner.Instance.SetData(null, (enemy) => { killedEnemies++; } );
            Spawner.EnemyZoneSpawner.Instance.Spawning = true;
            ShowScreen(gameScreen);
        }

        void Win()
        {
            ShowScreen(winScreen);
        }

        void Lose()
        {
            ShowScreen(loseScreen);
        }

        #endregion
    }
}
