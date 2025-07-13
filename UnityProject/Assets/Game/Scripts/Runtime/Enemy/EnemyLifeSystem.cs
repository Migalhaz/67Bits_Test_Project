using Game.Player;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemyLifeSystem : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] Collider m_enemyCollider;
        [SerializeField] EnemyController m_enemyController;
        [SerializeField] MeshRenderer m_mesh;

        [SerializeField] float m_timeInCollision = 2;
        float m_currentTime;

        bool m_inCollision;

        private void Awake()
        {
            m_currentTime = 0;
            m_inCollision = false;
        }

        private void OnEnable()
        {
            PlayerAttackObserverManager.m_OnEnterAttackCollision += EnterCollision;
            PlayerAttackObserverManager.m_OnExitAttackCollision += ExitCollision;
         
            PlayerStackerObserveManager.m_OnEnterStack += DisableCollider;
        }

        private void OnDisable()
        {
            PlayerAttackObserverManager.m_OnEnterAttackCollision -= EnterCollision;
            PlayerAttackObserverManager.m_OnExitAttackCollision -= ExitCollision;

            PlayerStackerObserveManager.m_OnEnterStack -= DisableCollider;
        }

        void Update()
        {
            TimeInCollision();
        }

        void TimeInCollision()
        {
            if (!m_inCollision)
            {
                return;
            }
            
            m_currentTime += Time.deltaTime;
            if (m_currentTime < m_timeInCollision)
            {
                return;
            }

            m_inCollision = false;
        }

        void EnterCollision(Collider collider)
        {
            if (m_inCollision)
            {
                return;
            }

            if (m_enemyCollider != collider)
            {
                return;
            }

            m_inCollision = true;
            
            if (m_enemyController.CheckCurrentState<EnemyDeadState>())
            {
                MoveToStack();
            }
            else
            {
                Death();
            }
        }

        void ExitCollision(Collider collider)
        {
            if (!m_inCollision)
            {
                return;
            }

            if (m_enemyCollider != collider)
            {
                return;
            }

            m_inCollision = false;
        }

        void Death()
        {
            m_mesh.material.color = Color.black;
            EnemyDeadState deadState = m_enemyController.m_DeadState;
            m_enemyController.ForceSwitchState(deadState);
            //transform.rotation = Quaternion.Euler(90, transform.eulerAngles.y, transform.eulerAngles.z);
        }

        void MoveToStack()
        {
            PlayerStackerObserveManager.RequestStack(transform);
        }

        void DisableCollider(Transform enemyTransform)
        {
            if (enemyTransform != transform)
            {
                return;
            }

            m_enemyCollider.enabled = false;
        }

        private void Reset()
        {
            TryGetComponent(out m_enemyCollider);
            TryGetComponent(out m_enemyController);
        }
    }
}
