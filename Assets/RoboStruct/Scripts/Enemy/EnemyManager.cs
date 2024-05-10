using System.Collections.Generic;
using UnityEngine;

namespace RoboStruct
{
    /// <summary>
    /// Deals with enemy spawning, despawning and tracking.
    /// </summary>
    public class EnemyManager : MonoBehaviourSingleton<EnemyManager>
    {
        [SerializeField] private GameObject _player;
        [SerializeField] private GameObject _enemy;
        [SerializeField] private List<Enemy> _enemiesAlive = new List<Enemy>();
        [SerializeField] private EnemySpawnPoint[] _enemySpawnPoints;
        private List<EnemySpawnPoint> _allowedEnemySpawnPoints = new List<EnemySpawnPoint>();

        private void Start()
        {
            Scene.Survival.Instance.OnStartWave += SpawnRandomEnemies;
        }

        public void SpawnRandomEnemies(double spawnEnemyCount)
        {
            Debug.Log("Spawning..");
            CheckAllowedEnemySpawnPoints();
            for (int i = 0; i < spawnEnemyCount; i++)
            {
                Enemy newEnemy = Instantiate(_enemy, PickAllowedEnemySpawnPoint(), Quaternion.identity).GetComponentInChildren<Enemy>();
                newEnemy.Target = _player;
                _enemiesAlive.Add(newEnemy);

                newEnemy.OnTakeDamage += AreAllEnemiesDead;
            }
        }

        private void AreAllEnemiesDead()
        {
            for (int i = 0; i < _enemiesAlive.Count; i++)
            {
                Enemy enemy = _enemiesAlive[i];
                if (enemy.IsDeadCheck() || enemy.IsDead || enemy == null)
                {
                    _enemiesAlive.Remove(enemy);
                    enemy.Died();
                    if (_enemiesAlive.Count < 1 && Scene.Survival.Instance.WaveInProgress) ClearedWave();
                }
            }
        }

        private void ClearedWave()
        {
            Scene.Survival.Instance.OnEndWave.Invoke();
            ClearAllowedEnemySpawnPoints();
        }

        private void CheckAllowedEnemySpawnPoints()
        {
            foreach (var _spawnPoint in _enemySpawnPoints)
            {
                if (!_spawnPoint.IsPlayerNearby)
                {
                    _allowedEnemySpawnPoints.Add(_spawnPoint);
                }
            }
        }

        private void ClearAllowedEnemySpawnPoints()
        {
            _allowedEnemySpawnPoints.Clear();
        }

        private Vector2 PickAllowedEnemySpawnPoint()
        {
            if (_allowedEnemySpawnPoints != null)
            {
                if (_allowedEnemySpawnPoints.Count > 0) return _allowedEnemySpawnPoints[Random.Range(0, _allowedEnemySpawnPoints.Count)].transform.position;
                else return _allowedEnemySpawnPoints[0].transform.position;
            }
            else
            {
                Debug.Log("PickAllowedEnemySpawnPoint List is null!");
                return new Vector2(0,0);
            }
        }
    }
}

