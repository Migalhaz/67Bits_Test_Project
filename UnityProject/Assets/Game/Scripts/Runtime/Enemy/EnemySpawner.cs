using UnityEngine;
using System.Collections.Generic;
using Game.Player;

namespace Game.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Spawn Settings")]
        [SerializeField] List<Transform> m_spawnPoints;
        [SerializeField, Min(0)] float m_minTimeToSpawn;
        [SerializeField, Min(0)] float m_maxTimeToSpawn;
        float m_currentTime;

        [SerializeField, Min(0)] int m_minSpawnCount;
        [SerializeField, Min(0)] int m_maxSpawnCount;

        [Header("Enemy Settings")]
        [SerializeField] EnemyController m_enemyController;
        [SerializeField] List<Transform> m_enemyMovePoints;
        int m_enemyCount;

        private void OnEnable()
        {
            EnemyObserverManager.m_OnDestroyEnemy += DecreaseEnemyCount;
            
        }

        private void OnDisable()
        {
            EnemyObserverManager.m_OnDestroyEnemy += DecreaseEnemyCount;
        }

        void DecreaseEnemyCount(EnemyController enemyController)
        {
            --m_enemyCount;
        }

        private void Start()
        {
            m_enemyCount = 0;
            ResetTimeValue();
        }

        private void Update()
        {
            DecreaseTime();
        }

        void DecreaseTime()
        {
            m_currentTime -= Time.deltaTime;
            if (m_currentTime <= 0)
            {
                ResetTimeValue();
                SpawnEnemy();
            }
        }

        void ResetTimeValue()
        {
            m_currentTime = Random.Range(m_minTimeToSpawn, m_maxTimeToSpawn);
        }

        void SpawnEnemy()
        {
            int currentStackCount = PlayerStackerObserveManager.RequestStackMaxCount() ?? 1;


            float stackCountBuffer = 1.5f;
            int maxEnemyCount = Mathf.RoundToInt(currentStackCount * stackCountBuffer);

            if (m_enemyCount >= maxEnemyCount)
            {
                return;
            }

            int spawnCount = Random.Range(m_minSpawnCount, m_maxSpawnCount);
            for (int i = 0; i < spawnCount; ++i)
            {
                if (m_enemyCount >= maxEnemyCount)
                {
                    break;
                }

                Transform spawnPoint = m_spawnPoints.GetRandom();
                EnemyController newEnemy = Instantiate(m_enemyController, spawnPoint.transform.position, Quaternion.identity);
                newEnemy.SetMoveStatePoints(m_enemyMovePoints);
                ++m_enemyCount;
            }
        }
    }
}
