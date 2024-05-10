using UnityEngine;

namespace RoboStruct
{
    /// <summary>
    /// Singleton that handles the player's movement. Depends on PlayerInput.
    /// </summary>
    public class PlayerLocomotion : MonoBehaviourSingleton<PlayerLocomotion>
    {
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private Player _player;
        private Rigidbody2D _playerRigidBody;

        private void Start()
        {
            _playerRigidBody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (_player.IsPlayerDead) _playerRigidBody.constraints = RigidbodyConstraints2D.None;
        }

        private void FixedUpdate()
        {
            if (!_player.IsPlayerDead) _playerRigidBody.MovePosition(playerInput.VirtualMouseRigidBody.position);
        }
    }
}
