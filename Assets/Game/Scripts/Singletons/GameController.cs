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
        [SerializeField] UI.UnitsShopScreen unitsShopScreen;
        [SerializeField] UI.Screen unitPlacementScreen;
        List<UI.Screen> screens;
        Stack<UI.Screen> screenHistory;
        UI.Screen currentScreen;

        [Header("Scene")]
        [SerializeField] Transform rightLevelBorder;
        [SerializeField] Fortress fortress;

        [Header("Game")]
        [SerializeField] int startCoins = 1000;
        [SerializeField] float levelTime = 120;
        int kills = 0;
        int coins = 0;
        int score = 0;
        float timer;

        [Header("Units")]
        [SerializeField] Unit[] unitPrefabs;

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
                unitsShopScreen,
                unitPlacementScreen,
            };
            screenHistory = new Stack<UI.Screen>();
        }

        private void Start()
        {
            gameScreen.Init(
                () => ShowScreen(pauseScreen),
                () => ShowScreen(settingsScreen),
                () => ShowScreen(unitsShopScreen),
                () => ShowScreen(helpScreen),
                () => ShowScreen(shopScreen)
                );
            pauseScreen.Init(LoadGame, LoadMenu, BackScreen);
            settingsScreen.Init(BackScreen);
            winScreen.Init(LoadGame, LoadMenu, null, null, null);
            loseScreen.Init(LoadGame, LoadMenu);
            helpScreen.Init(BackScreen);
            shopScreen.Init(BackScreen);
            unitsShopScreen.Init(unitPrefabs, BuyUnit, BackScreen);

            var camera = Camera.main;
            camera.transform.position = new Vector3(
                rightLevelBorder.position.x - camera.orthographicSize * camera.aspect,
                0,
                -5
            );

            StartGame();
        }

        private void Update()
        {
            timer -= Time.deltaTime;

            gameScreen.SetData(fortress.HealthFraction, score, timer, kills, coins);
            unitsShopScreen.SetData(coins, fortress.CanPlaceUnit);

            if (timer <= 0)
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
            fortress.onDeath += Lose;
            Spawner.EnemyZoneSpawner.Instance.SetData(null, (enemy) => { kills++; } );
            Spawner.EnemyZoneSpawner.Instance.Spawning = true;
            timer = levelTime;
            coins = startCoins;
            ShowScreen(gameScreen);
        }

        void Win()
        {
            Spawner.EnemyZoneSpawner.Instance.Reset();
            ShowScreen(winScreen);
        }

        void Lose()
        {
            Spawner.EnemyZoneSpawner.Instance.Reset();
            ShowScreen(loseScreen);
        }

        #endregion

        #region Units

        void BuyUnit(Unit unit)
        {
            coins -= unit.Price;
            ShowScreen(unitPlacementScreen);
            fortress.HighlightUnitPlacements((placement) => 
            {
                placement.SetUnit(Instantiate(unit));
                fortress.UnhighlightUnitPlacements();
                ShowScreen(gameScreen);
            });
        }

        #endregion
    }
}
