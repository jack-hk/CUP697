using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using RoboStruct.Scene;

namespace RoboStruct
{
    /// <summary>
    /// Singleton that should be used for scenes that has gameplay. Deals with gameplay UI functionaility and events.
    /// </summary>
    public class GameplayUI : MonoBehaviourSingleton<GameplayUI>
    {
        [SerializeField] private Player _player;
        [SerializeField] private GameObject _virtualGamepadUI;
        [SerializeField] private GameObject _hud;
        [SerializeField] private TextMeshProUGUI _levelCountetText;
        [SerializeField] private TextMeshProUGUI _waveCounterText;
        [SerializeField] private TextMeshProUGUI _statusText;
        [SerializeField] private Image _healthStatusHUD;
        private Color _statusTextOldColor;
        private Color _waveCounterOldColor;
        private bool _waveCounterIsFlashing = false;
        private bool _statusTextIsFlashing = false;
        private float _flashRate = 0.5f;

        private void Start()
        {
            SetVirtualGamepad(SettingsManager.Instance.CurrentSettings.IsVirtualGamepadEnabled);
            _player.OnTakenDamage += UpdateHealthStatus;
            if (Scene.Survival.Instance) Scene.Survival.Instance.OnStartWave += _ => FlashText(WaveCounterFlash(), _waveCounterText, _waveCounterOldColor, _waveCounterIsFlashing);
            if (Scene.Survival.Instance) Scene.Survival.Instance.OnStartWave += _ => UpdateWaveNumber();

            _waveCounterOldColor = _waveCounterText.color;
            _statusTextOldColor = _statusText.color;
            _levelCountetText.text = PlayerData.Instance.CurrentLevelProgress + "";
            _healthStatusHUD.color = Color.green;
        }

        public void SetVirtualGamepad(bool state)
        {
            if (!SettingsManager.Instance.CurrentSettings.IsVirtualGamepadEnabled) state = false;
            _virtualGamepadUI.SetActive(state);
        }

        public void DisableHUD(bool state)
        {
            _hud.SetActive(state);
        }

        private void UpdateHealthStatus()
        {
            float normalizedHealth = Mathf.Clamp01(_player.HitPoints / _player.MaxHitPoints);
            Color targetColor = Color.Lerp(Color.red, Color.green, normalizedHealth);
            _healthStatusHUD.color = targetColor;
            if (Mathf.FloorToInt(_player.HitPoints) < (Mathf.FloorToInt(_player.MaxHitPoints) / 4)) FlashText(StatusTextFlash(), _statusText, _statusTextOldColor, _statusTextIsFlashing);
        }

        private void UpdateWaveNumber()
        {
            _waveCounterText.text = Scene.Survival.Instance.CurrentWaveProgress + "";
        }

        private void FlashText(IEnumerator coroutine, TextMeshProUGUI text, Color oldColor, bool isFlashing)
        {
            if (!isFlashing)
            {
                isFlashing = true;
                StartCoroutine(coroutine);
            }
        }

        private void StopFlashing(IEnumerator coroutine, TextMeshProUGUI text, Color oldColor, bool isFlashing)
        {
            if (isFlashing)
            {
                isFlashing = false;
                text.color = oldColor;
                StopCoroutine(coroutine);
            }
        }

        private IEnumerator WaveCounterFlash()
        {
            for (int i = 0; i < 6; i++)
            {
                _waveCounterText.color = Color.red;
                yield return new WaitForSeconds(_flashRate);

                _waveCounterText.color = _waveCounterOldColor;
                yield return new WaitForSeconds(_flashRate);
            }
            StopFlashing(WaveCounterFlash(), _waveCounterText, _waveCounterOldColor, _waveCounterIsFlashing);
        }

        private IEnumerator StatusTextFlash()
        {
            while (true)
            {
                _statusText.color = Color.red;
                yield return new WaitForSeconds(_flashRate);

                _statusText.color = _waveCounterOldColor;
                yield return new WaitForSeconds(_flashRate);
            }
        }
    }
}
