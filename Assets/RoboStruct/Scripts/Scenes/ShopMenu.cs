using System;
using UnityEngine;
using UnityEngine.UI;

namespace RoboStruct.Scene
{
    /// <summary>
    /// Singletone scene manager that deals with the ShopMenu scene functions.
    /// "Scene managers" are scripts that handle with specific functions that only appear in that single scene.
    /// </summary>
    public class ShopMenu : MonoBehaviourSingleton<ShopMenu>
    {
        [SerializeField] private AudioManager _musicPlayer;
        [SerializeField] private Button[] _navigationButtons;
        [SerializeField] private GameObject[] _pages;

        private void Start()
        {
            _musicPlayer.Play("ShopTheme");
            InitaliseButtonEffects();
        }

        public void SwitchCurrentPage(int currentPage)
        {
            foreach (GameObject page in _pages)
            {
                page.SetActive(false);
            }
            _pages[currentPage].SetActive(true);

            foreach (Button button in _navigationButtons)
            {
                button.interactable = true;
            }
            _navigationButtons[currentPage].interactable = false;
        }

        private void InitaliseButtonEffects()
        {
            foreach (Button button in _navigationButtons)
            {
                button.onClick.AddListener(() => { SwitchCurrentPage(Array.IndexOf(_navigationButtons, button)); });
            }
        }
    }
}

