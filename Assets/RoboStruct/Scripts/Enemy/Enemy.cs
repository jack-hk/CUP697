using System;
using System.Collections;
using UnityEngine;

namespace RoboStruct
{
    /// <summary>
    /// Base class for Enemy actors. Interacts directly with the EnemyManager.
    /// </summary>
    public class Enemy : MonoBehaviour, IDamagable
    {
        [SerializeField] protected float _despawnTime = 3f;
        [SerializeField] private ActorCollider _actorCollider;

        [field: SerializeField] public float HitPoints { get; private set; }
        public bool IsDead { get; private set; }
        public GameObject Target { get; set; }


        public Action OnTakeDamage; // Event that updates EnemyManager tracking.

        private void Start()
        {
            _actorCollider.OnReceiveForceDamage += TakeDamage;
        }

        public bool IsDeadCheck()
        {
            if (HitPoints < 1 && !IsDead) return true;
            else return false;
        }

        public void TakeDamage(float damageAmount)
        {
            if (HitPoints < 1 && !IsDead)
            {
                IsDead = true;
                OnTakeDamage.Invoke();
            }
            else
            {
                HitPoints -= damageAmount;
                OnTakeDamage.Invoke();
            }
        }

        public void Died()
        {
            StartCoroutine(Dying());
        }

        protected virtual IEnumerator Dying()
        {
            yield return null;
        }
    }
}


