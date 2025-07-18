using UnityEngine;

namespace Game
{
    public class GameVisualEffects : MonoBehaviour
    {
        [SerializeField] GameObject m_levelUpEffectPrefab;
        [SerializeField] GameObject m_enemyDieEffectPrefab;
        [SerializeField] GameObject m_attackEffectPrefab;
        Transform m_playerTransform;

        private void OnEnable()
        {
            Enemy.EnemyObserverManager.m_OnDestroyEnemy += OnEnemyDie;
            Player.PlayerScoreObserverManager.m_OnBodyPriceLevelUp += OnLevelUp;
            Player.PlayerScoreObserverManager.m_OnStackLevelUp += OnLevelUp;
            Enemy.EnemyObserverManager.m_OnEnterDeadState += OnAtackEnemy;
        }

        private void OnDisable()
        {
            Enemy.EnemyObserverManager.m_OnDestroyEnemy -= OnEnemyDie;
            Player.PlayerScoreObserverManager.m_OnBodyPriceLevelUp -= OnLevelUp;
            Player.PlayerScoreObserverManager.m_OnStackLevelUp -= OnLevelUp;
            Enemy.EnemyObserverManager.m_OnEnterDeadState -= OnAtackEnemy;

        }

        private void Start()
        {
            m_playerTransform = Player.PlayerManager.m_Instance.transform ?? transform;
        }

        void OnEnemyDie(Enemy.EnemyController controller)
        {
            Vector3 spawnPosition = controller.m_VisualTransform.position;
            Instantiate(m_enemyDieEffectPrefab, spawnPosition, Quaternion.identity);
        }

        void OnLevelUp(int _)
        {
            Instantiate(m_levelUpEffectPrefab, m_playerTransform.position, m_playerTransform.rotation);
        }

        void OnAtackEnemy(Enemy.EnemyController controller)
        {
            Vector3 spawnPosition = controller.m_VisualTransform.position;
            Instantiate(m_attackEffectPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
