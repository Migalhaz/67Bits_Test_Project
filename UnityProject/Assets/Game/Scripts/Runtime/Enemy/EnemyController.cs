using UnityEngine;

namespace Game.Enemy
{
    public class EnemyController : StateMachineController
    {
        [SerializeField] EnemyIdleState m_idleState;
        [SerializeField] EnemyMoveState m_moveState;
        [SerializeField] EnemyDeadState m_deadState;

        public EnemyIdleState m_IdleState => m_idleState;
        public EnemyMoveState m_MoveState => m_moveState;
        public EnemyDeadState m_DeadState => m_deadState;

        protected override AbstractState FirstState() => m_idleState;

        private void Start()
        {
            StartController();
        }
    }
}
