using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

namespace RoboStruct
{
    /// <summary>
    /// Singleton that should be used for scenes that has gameplay. Deals with gameplay visual effects functionaility and events.
    /// </summary>
    public class GameplayVFX : MonoBehaviourSingleton<GameplayVFX>
    {
        [SerializeField] private GameObject _sparkContactEffect;
        [SerializeField] private int _poolSize = 10;

        private Queue<GameObject> _objectPool = new Queue<GameObject>();

        private void Start()
        {
            InitialiseObjectPooling();
        }

        private float Interpolate(float x, float inMin, float inMax, float outMin, float outMax)
        {
            return Mathf.Lerp(outMin, outMax, Mathf.InverseLerp(inMin, inMax, x));
        } //dupe in GameplaySFX

        public void ForceDamageContactEffect(float incomingDamageScale, Vector2 collisionPosition)
        {
            int damageScale = (int)Math.Round(incomingDamageScale);
            float calcSize = Interpolate(damageScale, 1, 40, 0, 0.5f);
            GameObject obj = GetSpark();
            obj.transform.localScale = new Vector3(calcSize, calcSize, 0);
            obj.transform.position = collisionPosition;
            StartCoroutine(DespawnTimer(obj));
        }

        public void ReturnSparkToPool(GameObject obj)
        {
            obj.SetActive(false);
            _objectPool.Enqueue(obj);
        }

        private GameObject GetSpark()
        {
            if (_objectPool.Count > 0)
            {
                GameObject obj = _objectPool.Dequeue();
                obj.SetActive(true);
                return obj;
            }
            else
            {
                GameObject newObj = Instantiate(_sparkContactEffect);
                return newObj;
            }
        }


        private void InitialiseObjectPooling()
        {
            for (int i = 0; i < _poolSize; i++)
            {
                GameObject obj = Instantiate(_sparkContactEffect);
                obj.SetActive(false);
                _objectPool.Enqueue(obj);
            }
        }

        private IEnumerator DespawnTimer(GameObject obj)
        {
            yield return new WaitForSeconds(0.6f);
            ReturnSparkToPool(obj);
        }
    }

}
