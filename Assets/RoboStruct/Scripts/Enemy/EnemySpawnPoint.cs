using UnityEngine;

namespace RoboStruct
{
    public class EnemySpawnPoint : MonoBehaviour
    {
        [SerializeField] private GameObject _player;
        [SerializeField] private float _playerDetectionRadius;
        public bool IsPlayerNearby { get; private set; } = false;

        private void Update()
        {
            IsPlayerNearby = CheckIfPlayerIsNearby();
        }

        private void OnDrawGizmos()
        {
            if (UnityEngine.Debug.isDebugBuild)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(gameObject.transform.position, 0.4f);
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (UnityEngine.Debug.isDebugBuild)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawWireSphere(gameObject.transform.position, _playerDetectionRadius);
            }
        }

        private bool CheckIfPlayerIsNearby()
        {
            if (Vector2.Distance(_player.transform.position, gameObject.transform.position) < _playerDetectionRadius)
            {
                return true;
            }
            return false;
        }
    }
}


