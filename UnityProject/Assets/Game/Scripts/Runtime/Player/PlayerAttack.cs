using UnityEngine;

namespace Game.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        [Header("Collider settings")]
        [SerializeField, Min(0)] Vector3 m_colliderSize;
        [SerializeField] Vector3 m_colliderOffset;
        [SerializeField] LayerMask m_enemyLayerMask;

        const int COLLISIONDETECTSIZE = 10;
        Collider[] m_enemiesInCollision = new Collider[COLLISIONDETECTSIZE];
        Vector3 m_colliderCenter => transform.TransformPoint(m_colliderOffset);

        private void FixedUpdate()
        {
            CheckCollision();
        }

        void CheckCollision()
        {
            
            int enemiesCount = Physics.OverlapBoxNonAlloc(m_colliderCenter, m_colliderSize, m_enemiesInCollision, Quaternion.identity, m_enemyLayerMask);
            for (int i = 0; i < COLLISIONDETECTSIZE; ++i)
            {
                Collider enemyCollider = m_enemiesInCollision[i];
                if (i < enemiesCount)
                {
                    PlayerAttackObserverManager.EnterAttackCollision(enemyCollider);
                }
                else
                {
                    if (enemyCollider != null)
                    {
                        PlayerAttackObserverManager.ExitAttackCollision(enemyCollider);
                        m_enemiesInCollision[i] = null;
                    }
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireCube(m_colliderCenter, m_colliderSize*2);
        }
    }
}
