using Game.Player;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemyState : AbstractState<EnemyController>
    {
        [SerializeField] protected Animator m_animator;
        [SerializeField] string m_animationName;
        [SerializeField, Min(0)] float m_animationCrossfade;
        public override void EnterState(StateMachineController stateMachineController)
        {
            base.EnterState(stateMachineController);
            m_animator.CrossFade(m_animationName, m_animationCrossfade);
        }
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
        [SerializeField] Transform m_visualTransform;
        [SerializeField] List<Transform> m_points;
        [SerializeField, Min(0)] float m_moveSpeed;
        [SerializeField, Min(0)] float m_rotationSpeed;

        Transform m_currentTargetPoint;
        StateTransition m_stateTransition;

        public override void EnterState(StateMachineController stateMachineController)
        {
            m_controller ??= stateMachineController as EnemyController;
            if (m_points == null || m_points.Count <= 0)
            {
                m_controller.ForceSwitchState(m_controller.m_IdleState);
                return;
            }

            base.EnterState(stateMachineController);

            m_currentTargetPoint = m_points.GetRandom();
            m_stateTransition ??= new StateTransition(InDistanceFromTarget, m_controller.m_IdleState, m_controller);
            AddTransition(m_stateTransition);
        }

        public void SetPoints(List<Transform> newPoints)
        {
            m_points = new List<Transform>(newPoints);
        }

        public override void UpdateState(StateMachineController stateMachineController)
        {
            base.UpdateState(stateMachineController);
            
            if (!m_currentTargetPoint)
            {
                m_controller.ForceSwitchState(m_controller.m_IdleState);
                return;
            }

            Vector3 moveDirection = GetMoveDirection();
            Move(moveDirection);
            Rotate(moveDirection);
        }

        Vector3 GetMoveDirection()
        {
            Vector3 enemyPosition = m_controller.transform.position.With(y: 0);
            Vector3 pointPosition = m_currentTargetPoint.position.With(y: 0);

            Vector3 result = (pointPosition - enemyPosition).normalized;
            return result;
        }

        void Move(Vector3 moveDirection)
        {
            Vector3 moveVector = Time.deltaTime * m_moveSpeed * moveDirection;
            m_controller.transform.Translate(moveVector, Space.World);
        }

        void Rotate(Vector3 moveDirection)
        {
            if (moveDirection.sqrMagnitude == 0)
            {
                return;
            }

            float currentAngle = m_visualTransform.eulerAngles.y;
            const float ANGLEOFFSET = 90;
            float moveAngle = ANGLEOFFSET + Mathf.Atan2(-moveDirection.z, moveDirection.x) * Mathf.Rad2Deg;
            float finalYAngle = Mathf.LerpAngle(currentAngle, moveAngle, m_rotationSpeed * Time.deltaTime);

            Quaternion result = Quaternion.Euler(0, finalYAngle, 0);
            m_visualTransform.rotation = result;
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
        [SerializeField] Rigidbody m_midleRig;
        [SerializeField] Collider m_collider;
        [SerializeField, Min(0)] float m_lifeTimeAfterExitStack;
        [SerializeField, Min(0)] float m_lifeTimeOutStack;
        float m_currentLifeTimeOutStack;
        [SerializeField] Transform m_transformToStack;
        bool m_wasPopFromStack;
        bool m_inStack;
        public override void EnterState(StateMachineController stateMachineController)
        {
            m_collider.isTrigger = true;
            m_wasPopFromStack = false;
            base.EnterState(stateMachineController);
            m_animator.enabled = false;
            m_inStack = false;
            m_currentLifeTimeOutStack = m_lifeTimeOutStack;
            EnemyObserverManager.EnterDeadState(m_controller);
            PlayerStackerObserveManager.m_OnEnterStack += OnEnterStack;
            PlayerStackerObserveManager.m_OnExitStack += OnPopFromStack;
        }

        public override void UpdateState(StateMachineController stateMachineController)
        {
            base.UpdateState(stateMachineController);
            if (!m_inStack)
            {
                m_currentLifeTimeOutStack -= Time.deltaTime;

                if (m_currentLifeTimeOutStack <= 0)
                {
                    m_controller.DestroyEnemy(0);
                    return;
                }
            }

            if (!m_wasPopFromStack)
            {
                return;
            }
            
            m_controller.DestroyEnemy(m_lifeTimeAfterExitStack);
        }

        void OnEnterStack(Transform enemyTransform)
        {
            if (m_wasPopFromStack)
            {
                return;
            }
            if (enemyTransform != m_transformToStack)
            {
                return;
            }
            m_inStack = true;
            m_midleRig.isKinematic = true;
            m_collider.enabled = false;
        }

        void OnPopFromStack(Transform enemyTransform)
        {
            if (enemyTransform != m_transformToStack)
            {
                return;
            }
            m_midleRig.isKinematic = false;
            m_wasPopFromStack = true;
        }

        public override void ExitState(StateMachineController stateMachineController)
        {
            base.ExitState(stateMachineController);
            PlayerStackerObserveManager.m_OnEnterStack -= OnEnterStack;
            PlayerStackerObserveManager.m_OnExitStack -= OnPopFromStack;
        }
    }
}
