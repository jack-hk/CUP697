
namespace RoboStruct
{
    /// <summary>
    /// Persistent singleton that globally manages settings data between scenes.
    /// </summary>
    public class SettingsManager : MonoBehaviourSingletonPersistent<SettingsManager>
    {
        public class Settings
        {
            public bool IsVirtualGamepadEnabled { get; set; }
            public bool TutorialIsActive { get; set; }
        }

        public Settings DefaultSettings { get; private set; } = new Settings();
        public Settings CurrentSettings { get; private set; } = new Settings();

        private void Start()
        {
            SetDefaultSettings();
            UpdateToDefaults();
        }

        private void SetDefaultSettings()
        {
            DefaultSettings.TutorialIsActive = true;
        }

        private void UpdateToDefaults()
        {
            CurrentSettings.TutorialIsActive = DefaultSettings.TutorialIsActive;
        }
    }
}

