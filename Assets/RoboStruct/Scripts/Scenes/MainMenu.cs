using UnityEngine;
using UnityEngine.UI;

namespace RoboStruct.Scene
{
    /// <summary>
    /// Singletone scene manager that deals with the MainMenu scene functions.
    /// "Scene managers" are scripts that handle with specific functions that only appear in that single scene.
    /// </summary>
    public class MainMenu : MonoBehaviourSingleton<GameManager>
    {
        [SerializeField] private AudioManager _musicPlayer;
        [SerializeField] private GameObject _titleScreen;
        [SerializeField] private GameObject _settingsMenu;

        [Header("UI Elements")]
        [SerializeField] private Toggle _virtualGamepadToggle;
        [SerializeField] private Toggle _tutorialToggle;

        private void Start()
        {
            PlayMusic();
            if (!RuntimePlatformCompatibility.Instance.IsVirtualGamepadAllowed) DisallowVirtualGamepadToggle();
            UpdateSettingsToSaved();
        }

        public void DisallowVirtualGamepadToggle()
        {
            SetVirtualGamepad(true);
            _virtualGamepadToggle.interactable = false;
        }

        public void SetSettingsMenu(bool state) // Used in UnityEvents
        {
            _titleScreen.SetActive(!state);
            _settingsMenu.SetActive(state);
        }

        public void SetVirtualGamepad(bool state)
        {
            SettingsManager.Instance.CurrentSettings.IsVirtualGamepadEnabled = state;
        }

        public void ToggleTutorial(bool state)
        {
            SettingsManager.Instance.CurrentSettings.TutorialIsActive = state;
        }

        private void PlayMusic()
        {
            _musicPlayer.Play("TitleScreen");
        }

        private void UpdateSettingsToSaved() // Sends the data to the persistent SettingsManager
        {
            _virtualGamepadToggle.isOn = SettingsManager.Instance.CurrentSettings.IsVirtualGamepadEnabled;
            _tutorialToggle.isOn = SettingsManager.Instance.CurrentSettings.TutorialIsActive;
        }
    }
}