using TMPro;
using UnityEngine;

namespace RoboStruct
{
    public class HUDHealth : MonoBehaviour
    {
        [SerializeField] UnityEngine.UI.Image _currentArmour;
        [SerializeField] TextMeshProUGUI _healthPercentage;
        [SerializeField] Player _player;

        private void Start()
        {
            _player.OnTakenDamage += UpdatePercent;

            _currentArmour.sprite = PlayerData.Instance.EquippedArmour.Sprite;
        }

        void UpdatePercent()
        {
            if (Mathf.FloorToInt((_player.HitPoints / _player.MaxHitPoints) * 100f) < 1) _healthPercentage.text = "ERROR";
            else _healthPercentage.text = Mathf.FloorToInt((_player.HitPoints / _player.MaxHitPoints) * 100f) + "%";
            
        }
    }
}

