using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RoboStruct
{
    /// <summary>
    /// Deals with loading scenes and waiting screens.
    /// </summary>
    public class SceneLoader : MonoBehaviourSingletonPersistent<SceneLoader>
    {
        [SerializeField] private GameObject _loadingScreen;
        [SerializeField] private Image _progressBar;

        public enum Scene
        {
            MainMenu,
            Survival,
            ShopMenu
        }

        public void Load(Scene scene)
        {
            StartCoroutine(LoadAsyncCoroutine(scene.ToString()));
        }

        private IEnumerator LoadAsyncCoroutine(string scene)
        {
            AsyncOperation asyncLoading = SceneManager.LoadSceneAsync(scene);

            _loadingScreen.SetActive(true);

            while (!asyncLoading.isDone)
            {
                float progressValue = Mathf.Clamp01(asyncLoading.progress / 0.9f);
                _progressBar.fillAmount = progressValue;

                yield return null;
            }

            _loadingScreen.SetActive(false);
        }
    }
}
