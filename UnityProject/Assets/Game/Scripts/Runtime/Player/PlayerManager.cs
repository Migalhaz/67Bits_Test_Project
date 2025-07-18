using UnityEngine;

namespace Game.Player
{
    [DefaultExecutionOrder(-110)]
    public class PlayerManager : Singleton<PlayerManager>
    {
        [Header("Unity Components")]
        [SerializeField] Rigidbody m_rigidbody;
        [SerializeField] CapsuleCollider m_playerCollider;

        [Header("Player Scripts")]
        [SerializeField] PlayerAnimation m_playerAnimation;
        [SerializeField] PlayerAttack m_playerAttack;
        [SerializeField] PlayerMove m_playerMove;
        [SerializeField] PlayerScore m_playerScore;
        [SerializeField] PlayerStacker m_playerStacker;
        [SerializeField] PlayerVisual m_playerVisual;


        public Rigidbody m_Rigidbody => m_rigidbody;
        public CapsuleCollider m_PlayerCollider => m_playerCollider;



        protected override void Awake()
        {
            base.Awake();
            GetPlayerComponents();
        }

        void GetPlayerComponents()
        {
            if (!m_rigidbody)
            {
                TryGetComponent(out m_rigidbody);
            }

            if (!m_playerCollider)
            {
                TryGetComponent(out m_playerCollider);
            }

            if (!m_playerAnimation)
            {
                TryGetComponent(out m_playerAnimation);
            }

            if (!m_playerAttack)
            {
                TryGetComponent(out m_playerAttack);
            }

            if (!m_playerMove)
            {
                TryGetComponent(out m_playerMove);
            }

            if (!m_playerScore)
            {
                TryGetComponent(out m_playerScore);
            }

            if (!m_playerStacker)
            {
                TryGetComponent(out m_playerStacker);
            }
            
            if (!m_playerVisual)
            {
                TryGetComponent(out m_playerVisual);
            }
        }

        private void Reset()
        {
            GetPlayerComponents();
        }
    }
}
