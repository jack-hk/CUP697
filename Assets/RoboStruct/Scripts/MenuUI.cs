using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace RoboStruct
{
    /// <summary>
    /// Should be used for scenes that have menus. Deals with menu UI functionaility and events.
    /// </summary>
    public class MenuUI : MonoBehaviour
    {
        [SerializeField] private AudioManager UISounds;

        private void Start()
        {
            FindAndListenToButtons();
            FindAndListenToToggles();
        }

        private void FindAndListenToButtons()
        {
            Button[] allowedButtons = FindObjectsOfType<Button>(true);
            foreach (Button button in allowedButtons)
            {
                button.onClick.AddListener(OnElementPressed);
            }
        }

        private void FindAndListenToToggles()
        {
            Toggle[] allowedToggles = FindObjectsOfType<Toggle>(true);
            foreach (Toggle toggle in allowedToggles)
            {
                toggle.onValueChanged.AddListener(OnBoolElementPressed);
            }
        }

        private void OnElementPressed()
        {
            UISounds.Play("Click");
        }

        private void OnBoolElementPressed(bool isOn)
        {
            if (isOn) OnBoolElementEnabled();
            else OnBoolElementDisabled();
        }

        private void OnBoolElementEnabled()
        {
            UISounds.Play("Toggle", 1f, 1.5f);
        }

        private void OnBoolElementDisabled()
        {
            UISounds.Play("Toggle", 1f, 0.75f);
        }
    }
}
