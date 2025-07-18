using UnityEngine;

namespace Game.Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] Animator m_animator;
        [SerializeField] string m_moveBoolName;
        [SerializeField] string m_blowKissTriggerName;
        [SerializeField] string m_victoryTriggerName;

        bool m_isMoving;


        private void OnEnable()
        {
            Enemy.EnemyObserverManager.m_OnEnterDeadState += KillEnemy;
            PlayerScoreObserverManager.m_OnStackLevelUp += PlayVictory;
            PlayerScoreObserverManager.m_OnBodyPriceLevelUp += PlayVictory;

            PlayerInputObserverManager.m_OnMoveInput += MoveInput;
        }

        private void OnDisable()
        {
            Enemy.EnemyObserverManager.m_OnEnterDeadState -= KillEnemy;
            PlayerScoreObserverManager.m_OnStackLevelUp -= PlayVictory;
            PlayerScoreObserverManager.m_OnBodyPriceLevelUp -= PlayVictory;

            PlayerInputObserverManager.m_OnMoveInput -= MoveInput;
        }

        void KillEnemy(Enemy.EnemyController controler)
        {
            m_animator.SetTrigger(m_blowKissTriggerName);
        }

        void PlayVictory(int _)
        {
            m_animator.SetTrigger(m_victoryTriggerName);
        }

        void MoveInput(Vector3 moveDirection)
        {
            m_isMoving = moveDirection.sqrMagnitude != 0;
            m_animator.SetBool(m_moveBoolName, m_isMoving);
        }
    }
}
