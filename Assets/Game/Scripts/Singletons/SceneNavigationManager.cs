using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class SceneNavigationManager : Singleton<SceneNavigationManager>
    {
		[SerializeField] string menuScene = "Menu";
        [SerializeField] string gameScene = "Game";

        public void QuitApplication() => Application.Quit();
        public void LoadMenu(System.Action onLoaded = null) => LoadScene(menuScene, onLoaded);
        public void LoadGame(System.Action onLoaded = null) => LoadScene(gameScene, onLoaded);

        void LoadScene(string sceneName, System.Action onLoaded = null)
		{
			StartCoroutine(SceneLoadingCoroutine(sceneName, onLoaded));
		}

		IEnumerator SceneLoadingCoroutine(string sceneName, System.Action onLoaded = null)
		{
			var loading = SceneManager.LoadSceneAsync(sceneName);
			yield return new WaitUntil(() => loading.isDone);

            onLoaded?.Invoke();
        }
    }
}
