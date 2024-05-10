using System;
using UnityEngine;

namespace RoboStruct
{
    /// <summary>
    /// Deals with collision functions. This includes incoming force damages and other types of damage.
    /// </summary>
    public class ActorCollider : MonoBehaviour
    {
        private float _collisionCooldown = 0.6f;
        private bool _canCollide = true;

        [field: SerializeField] public float ForceDamageMultiplier { get; set; } = 1;
        public float ForceDamageMinimum { get; private set; } = 5;
        public float ForceDamageMaximum { get; private set; } = 100;

        public Action<float> OnReceiveForceDamage;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (_canCollide) 
            {
                ReceiveForceDamage(collision);
                Invoke("Timer", _collisionCooldown);
                _canCollide = false;
            }
            for (int i = 0; i < collision.contactCount; i++)
            {
                ContactPoint2D contactPoint = collision.GetContact(i);
                GameplayVFX.Instance.ForceDamageContactEffect(collision.relativeVelocity.magnitude, contactPoint.point);
            }
        }

        public void ReceiveForceDamage(Collision2D collision)
        {
            float forceMagnitude = collision.relativeVelocity.magnitude;
            float damage = forceMagnitude * ForceDamageMultiplier;

            if (damage > ForceDamageMinimum)
            {
                GameplaySFX.Instance.ForceDamageSoundEffect(forceMagnitude, collision.GetContact(0).point);
                if (damage >= ForceDamageMaximum) OnReceiveForceDamage.Invoke(ForceDamageMaximum);
                else OnReceiveForceDamage.Invoke(damage);
            }
        }

        private void Timer()
        {
            _canCollide = true;
        }
    }
}

