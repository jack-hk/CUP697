using RoboStruct.Scene;
using UnityEngine;

namespace RoboStruct
{
    /// <summary>
    /// Singleton that manages tutorial functions.
    /// </summary>
    public class TutorialManager : MonoBehaviourSingleton<TutorialManager>
    {
        [SerializeField] GameObject _tutorialWelcome;

        private void Start()
        {
            if (Survival.Instance.CurrentWaveProgress == 1 && PlayerData.Instance.CurrentLevelProgress == 1 && SettingsManager.Instance.CurrentSettings.TutorialIsActive) EnableTutorialWelcome();
        }

        public void DisableTutorialWelcome()
        {
            _tutorialWelcome.SetActive(false);
            SettingsManager.Instance.CurrentSettings.TutorialIsActive = false;
            if (SettingsManager.Instance.CurrentSettings.IsVirtualGamepadEnabled) GameManager.Instance.GameStateFSM.TransitionTo(GameManager.Instance.GameplayStateCallback);
        }

        private void EnableTutorialWelcome()
        {
            _tutorialWelcome.SetActive(true);
            if (SettingsManager.Instance.CurrentSettings.IsVirtualGamepadEnabled) GameManager.Instance.GameStateFSM.TransitionTo(GameManager.Instance.MenuStateCallback);
        }
    }

}
