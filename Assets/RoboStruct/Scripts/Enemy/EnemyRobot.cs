using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace RoboStruct
{
    /// <summary>
    /// Derives from Enemy. Robot enemies are similar to the player, they act in almost the same way, using physics to make attacks.
    /// </summary>
    public class EnemyRobot : Enemy
    {
        [SerializeField] private float _smokeThreshold = 25f;
        [SerializeField] private float _heavySmokeThreshold = 75f;
        [SerializeField] private float _aggressionRange = 7f;
        [SerializeField] private float _attackingSpeed = 12f;
        [SerializeField] private float _attackingRadius = 1f;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private Rigidbody2D _rigidBody;
        [SerializeField] private ParticleSystem _smoke;
        [SerializeField] private ParticleSystem _heavySmoke;

        private void Awake()
        {
            _navMeshAgent.updateRotation = false;
            _navMeshAgent.updateUpAxis = false;
        }

        private void Update()
        {
            if (!IsDead && _navMeshAgent && _navMeshAgent.enabled == true) _navMeshAgent.SetDestination(Target.transform.position);
        }

        private void FixedUpdate()
        {
            if (Vector2.Distance(_rigidBody.position, Target.transform.position) < _aggressionRange && !IsDead) SwingAttack(); // If within range
            if (HitPoints < _smokeThreshold) _smoke.gameObject.SetActive(true);
            if (HitPoints < _heavySmokeThreshold) _heavySmoke.gameObject.SetActive(true);
        }

        private void SwingAttack()
        {
            float angle = Time.time * _attackingSpeed; // Calculate based on time and speed
            float x = Mathf.Cos(angle) * _attackingRadius;
            float y = Mathf.Sin(angle) * _attackingRadius;
            Vector2 velocity = new Vector2(x, y);
            _rigidBody.velocity = velocity;
        }

        protected override IEnumerator Dying()
        {
            _navMeshAgent.enabled = false;
            _rigidBody.gravityScale = 2;
            _rigidBody.constraints = RigidbodyConstraints2D.None;
            _rigidBody.velocity = Vector2.zero;
            _rigidBody.velocity = new Vector2(0, -4);
            yield return new WaitForSeconds(_despawnTime);
            Destroy(gameObject);
        }
    }
}

