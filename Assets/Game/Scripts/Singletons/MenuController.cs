using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class MenuController : Singleton<MenuController>
    {
        [Header("UI")]
        [SerializeField] UI.MenuScreen menuScreen;
        [SerializeField] UI.LevelSelectionScreen levelSelectionScreen;
        List<UI.Screen> screens;

        private void Awake()
        {
            screens = new List<UI.Screen>()
            {
                menuScreen,
                levelSelectionScreen,
            };
        }

        private void Start()
        {
            menuScreen.Init(
                SceneNavigationManager.Instance.QuitApplication,
                () => { throw new System.NotFiniteNumberException(); },
                () => ShowScreen(levelSelectionScreen)
                );
            levelSelectionScreen.Init(
                () => ShowScreen(menuScreen),
                () => SceneNavigationManager.Instance.LoadGame()
                );

            ShowScreen(menuScreen);
        }

        #region UI

        void ShowScreen(UI.Screen screen)
        {
            screens.ForEach(s => s.Hide());
            screen.Show();
        }

        #endregion
    }
}
