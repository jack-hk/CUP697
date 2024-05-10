using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace RoboStruct.Scene
{
    /// <summary>
    /// Singletone scene manager that deals with the Survival scene functions.
    /// "Scene managers" are scripts that handle with specific functions that only appear in that single scene.
    /// 
    /// The Survival scene has functionality that includes wave progression.
    /// </summary>
    public class Survival : MonoBehaviourSingleton<Survival>
    {
        [SerializeField] private Player _player;
        [SerializeField] private GameObject _survivalDeathScreenUI; // There are planned to be different death screens depending on the mode (survival, etc).
        [SerializeField] private GameObject _tutorialWelcomeUI;
        [SerializeField] private Image _survivalDeathScreenBackground;
        [SerializeField] private float _secondsBreakBetweenWaves;

        /// <summary>
        /// Waves of enemies occur within levels, sometimes multiple times.
        /// </summary>
        private float _elapsedWaveTime = 0; // Also used for discoveries menu.
        private float _elapsedLevelTime = 0;

        public int CurrentWaveProgress { get; private set; } = 0;
        public bool WaveInProgress { get; private set; } = false;

        public double AmountOfSpawnedEnemies
        {
            get { return Math.Round(PlayerData.Instance.WaveDifficultyScale * 0.6); }
        }

        public Action<double> OnStartWave;
        public Action OnEndWave;

        private void Start()
        {
            if (RuntimePlatformCompatibility.Instance.IsInUnityEditor) OnStartWave += PrintDifficulty;
            OnEndWave += WaveEnded;
            _player.OnPlayerDied += PlayerDied;
            StartWave();
        }

        public void StartWave()
        {
            WaveInProgress = true;
            CurrentWaveProgress++;
            PlayerData.Instance.AdvanceWaveDifficultyScale();
            if (RuntimePlatformCompatibility.Instance.IsInUnityEditor) Debug.Log("Wave started!");

            if (CurrentWaveProgress <= 3) OnStartWave.Invoke(AmountOfSpawnedEnemies);
            else AdvanceToNextLevel();
        }

        public void PrintDifficulty(double difficulty)
        {
            Debug.Log(difficulty);
        }

        public void WaveEnded()
        {
            WaveInProgress = false;
            if (RuntimePlatformCompatibility.Instance.IsInUnityEditor) Debug.Log("Wave completed!");
            StartCoroutine(NextWaveTimer());
        }

        public void AdvanceToNextLevel()
        {
            if (!_player.IsPlayerDead)
            {
                SceneLoader.Instance.Load(SceneLoader.Scene.ShopMenu);
                PlayerData.Instance.CurrentLevelProgress++;
            }
        }
        public void ResetWavesInLevel()
        {
            PlayerData.Instance.SubtractWaveDifficultyScale(CurrentWaveProgress);
        }

        #region UI Functions
        public void ExitToMainMenu()
        {
            if (!_player.IsPlayerDead) ResetWavesInLevel();
            SceneCommon.Instance.LoadMainMenu();
        }

        public void OpenPauseMenu()
        {
            GameManager.Instance.GameStateFSM.TransitionTo(GameManager.Instance.MenuStateCallback);
        }

        public void ResumeGame()
        {
            GameManager.Instance.GameStateFSM.TransitionTo(GameManager.Instance.GameplayStateCallback);
        }

        private IEnumerator OpenDeathScreen()
        {
            if (_tutorialWelcomeUI.activeInHierarchy) _tutorialWelcomeUI.SetActive(false);
            _survivalDeathScreenUI.SetActive(true);
            float fadeInDuration = 3f;
            float elapsedTime = 0f;
            Color color = _survivalDeathScreenBackground.color;
            while (elapsedTime < fadeInDuration)
            {
                elapsedTime += Time.deltaTime;
                color.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeInDuration);
                _survivalDeathScreenBackground.color = color;
                yield return null;
            }
            color.a = 1f;
            _survivalDeathScreenBackground.color = color;
            GameManager.Instance.GameStateFSM.TransitionTo(GameManager.Instance.MenuStateCallback);
        }
        #endregion

        private IEnumerator NextWaveTimer()
        {
            if (RuntimePlatformCompatibility.Instance.IsInUnityEditor) Debug.Log("Next wave is coming soon..");
            yield return new WaitForSeconds(_secondsBreakBetweenWaves);
            StartWave();
        }

        private void PlayerDied()
        {
            StartCoroutine(OpenDeathScreen());
            PlayerData.Instance.ClearData();
        }
    }
}