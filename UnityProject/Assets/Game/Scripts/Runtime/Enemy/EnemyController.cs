using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemyController : StateMachineController
    {
        [field: SerializeField] public Transform m_VisualTransform { get; private set; }
        [SerializeField] EnemyIdleState m_idleState;
        [SerializeField] EnemyMoveState m_moveState;
        [SerializeField] EnemyDeadState m_deadState;

        public EnemyIdleState m_IdleState => m_idleState;
        public EnemyMoveState m_MoveState => m_moveState;
        public EnemyDeadState m_DeadState => m_deadState;

        protected override AbstractState FirstState() => m_idleState;

        private void Awake()
        {
            StartController();
        }

        public void DestroyEnemy(float timeToDestroy)
        {
            StopController();
            StartCoroutine(TimeToDestroy(timeToDestroy));
        }

        IEnumerator TimeToDestroy(float timeToDestroy)
        {
            if (timeToDestroy > 0)
            {
                yield return Extensions.GetWaitForSeconds(timeToDestroy);
            }
            EnemyObserverManager.DestroyEnemy(this);
            Destroy(gameObject);
        }

        public void SetMoveStatePoints(List<Transform> points)
        {
            m_moveState.SetPoints(points);
        }
    }

    public static class EnemyObserverManager
    {
        public static System.Action<EnemyController> m_OnEnterDeadState = null;
        public static System.Action<EnemyController> m_OnDestroyEnemy = null;

        public static void EnterDeadState(EnemyController controller) => m_OnEnterDeadState?.Invoke(controller);
        public static void DestroyEnemy(EnemyController controller) => m_OnDestroyEnemy?.Invoke(controller);
    }
}
