using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemyState : AbstractState<EnemyController>
    {
        
    }

    [System.Serializable]
    public class EnemyIdleState : EnemyState
    {
        [SerializeField, Min(0)] float m_minTimeOnState;
        [SerializeField, Min(0)] float m_maxTimeOnState;
        float m_currentTime;
        StateTransition m_stateTransition;
        public override void EnterState(StateMachineController stateMachineController)
        {
            base.EnterState(stateMachineController);
            m_currentTime = Random.Range(m_minTimeOnState, m_maxTimeOnState);

            m_stateTransition ??= new StateTransition(OutStateTime, m_controller.m_MoveState, stateMachineController);
            m_transitionController.AddTransition(m_stateTransition);
        }

        bool OutStateTime()
        {
            m_currentTime -= Time.deltaTime;
            return m_currentTime <= 0;
        }
    }

    [System.Serializable]
    public class EnemyMoveState : EnemyState
    {
        [SerializeField] List<Transform> m_points;
        [SerializeField] float m_moveSpeed;

        Transform m_currentTargetPoint;
        StateTransition m_stateTransition;

        public override void EnterState(StateMachineController stateMachineController)
        {
            base.EnterState(stateMachineController);

            if (m_points == null || m_points.Count <= 0)
            {
                m_controller.ForceSwitchState(m_controller.m_IdleState);
                return;
            }

            m_currentTargetPoint = m_points.GetRandom();
            m_stateTransition ??= new StateTransition(InDistanceFromTarget, m_controller.m_IdleState, m_controller);
            AddTransition(m_stateTransition);
        }

        public override void UpdateState(StateMachineController stateMachineController)
        {
            base.UpdateState(stateMachineController);

            Vector3 enemyPosition = m_controller.transform.position.With(y: 0);
            Vector3 pointPosition = m_currentTargetPoint.position.With(y: 0);

            Vector3 moveDirection = (pointPosition - enemyPosition).normalized;
            Vector3 moveVector = Time.deltaTime * m_moveSpeed * moveDirection;

            m_controller.transform.Translate(moveVector, Space.World);
        }

        bool InDistanceFromTarget()
        {
            Vector3 enemyPosition = m_controller.transform.position.With(y: 0);
            Vector3 pointPosition = m_currentTargetPoint.position.With(y: 0);

            float distance = Vector3.Distance(enemyPosition, pointPosition);

            return distance < 1;
        }
    }

    [System.Serializable]
    public class EnemyDeadState : EnemyState
    {

    }
}
