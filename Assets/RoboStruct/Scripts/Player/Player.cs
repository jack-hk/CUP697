using UnityEngine;
using System;
using System.Collections;

namespace RoboStruct
{
    /// <summary>
    /// Class that handles the player's common functions.
    /// </summary>
    public class Player : MonoBehaviour, IDamagable
    {
        [SerializeField] private SpriteRenderer _propellerSprite;  
        [SerializeField] private SpriteRenderer _armourSprite;
        [SerializeField] private ActorCollider _actorCollider;
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private float _playeDiedCinematicLength = 5f;
        [SerializeField] private Rope _playerRope;
        private float _armourDamageMultipler = 1;

        [field: SerializeField] public float MaxHitPoints { get; private set; } = 100;
        public float HitPoints { get; private set; }
        public bool IsPlayerDead { get; private set; }

        public Action OnPlayerDied;
        public Action OnTakenDamage;

        private void Start()
        {
            _actorCollider.OnReceiveForceDamage += TakeDamage;
            IsPlayerDead = false;

            InitialiseWeapon();
            InitialiseArmour();
            InitialisePropeller();

            HitPoints = MaxHitPoints;
        }

        public void TakeDamage(float damageAmount)
        {
            HitPoints -= damageAmount * _armourDamageMultipler;
            if (HitPoints < 1) Died();
            OnTakenDamage.Invoke();
        }

        public void Died()
        {
            if (IsPlayerDead) return;
            IsPlayerDead = true;
            StartCoroutine(DeadCinematic());
        }

        private IEnumerator DeadCinematic()
        {
            GameManager.Instance.GameStateFSM.TransitionTo(GameManager.Instance.CinematicStateCallback);
            yield return new WaitForSeconds(_playeDiedCinematicLength);
            OnPlayerDied.Invoke();
        }

        private void InitialiseWeapon()
        {
            _playerRope.weaponPart.GetComponent<Rigidbody2D>().mass = PlayerData.Instance.EquippedWeapon.Mass;
            _playerRope.weaponPart.GetComponent<HingeJoint2D>().useLimits = PlayerData.Instance.EquippedWeapon.LimitAngle;
            _playerRope.weaponPart.GetComponent<SpriteRenderer>().sprite = PlayerData.Instance.EquippedWeapon.Sprite;
        }

        private void InitialiseArmour()
        {
            MaxHitPoints = PlayerData.Instance.EquippedArmour.HitPoints;
            _actorCollider.ForceDamageMultiplier = PlayerData.Instance.EquippedArmour.ForceMultiplier;
            _armourSprite.sprite = PlayerData.Instance.EquippedArmour.Sprite;
        }

        private void InitialisePropeller()
        {
            _playerInput.SpeedMultipler = PlayerData.Instance.EquippedPropeller.SpeedMultiplier;
            _propellerSprite.sprite = PlayerData.Instance.EquippedPropeller.Sprite;
        }
    }

}

