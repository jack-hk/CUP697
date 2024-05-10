using System;
using UnityEngine;

namespace RoboStruct
{
    /// <summary>
    /// Singleton that should be used for scenes that has gameplay. Deals with gameplay audio functionaility and events.
    /// </summary>
    public class GameplaySFX : MonoBehaviourSingleton<GameplaySFX>
    {
        [SerializeField] private AudioManager _effectSFX;
        [SerializeField] private AudioManager _musicSFX;
        [SerializeField] private GameObject _playerGameObject;
        [SerializeField] private Vector2 _minPitch = new Vector2(1, 4.5f);
        [SerializeField] private Vector2 _maxPitch = new Vector2(40, 0.8f);
        [SerializeField] private Vector2 _minVol = new Vector2(1, 1);
        [SerializeField] private Vector2 _maxVol = new Vector2(10, 0);

        private float Interpolate(float x, float inMin, float inMax, float outMin, float outMax)
        {
            return Mathf.Lerp(outMin, outMax, Mathf.InverseLerp(inMin, inMax, x));
        }

        public void ForceDamageSoundEffect(float incomingDamageScale, Vector2 collisionPosition)
        {
            int damageScale = (int)Math.Round(incomingDamageScale);
            float calcPitch = Interpolate(damageScale, _minPitch.x, _maxPitch.x, _minPitch.y, _maxPitch.y);
            float calcVolume = Interpolate(Vector3.Distance(_playerGameObject.transform.position, collisionPosition), _minVol.x, _maxVol.x, _minVol.y, _maxVol.y);
            if (UnityEngine.Random.Range(1, 2) == 1) _effectSFX.Play("MetalHit1", calcVolume, calcPitch);
            else _effectSFX.Play("MetalHit2", calcVolume, calcPitch);

        }

        private void Start()
        {
            _musicSFX.Play("BattleMusic1");
        }
    }
}

