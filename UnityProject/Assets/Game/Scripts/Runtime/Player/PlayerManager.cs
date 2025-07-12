using UnityEngine;

namespace Game.Player
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        [Header("Unity Components")]
        [SerializeField] Rigidbody m_rigidbody;
        [SerializeField] CapsuleCollider m_playerCollider;
        [SerializeField] UnityEngine.InputSystem.PlayerInput m_playerInput;

        [Header("Player Scripts")]
        [SerializeField] PlayerInputListener m_playerInputListener;


        public Rigidbody m_Rigidbody => m_rigidbody;
        public CapsuleCollider m_PlayerCollider => m_playerCollider;
        public UnityEngine.InputSystem.PlayerInput m_PlayerInput => m_playerInput;


        public PlayerInputListener m_PlayerInputListener => m_playerInputListener;


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

            if (!m_playerInput)
            {
                TryGetComponent(out m_playerInput);
            }

            if (!m_playerInputListener)
            {
                TryGetComponent(out m_playerInputListener);
            }
        }

        private void Reset()
        {
            GetPlayerComponents();
        }
    }
}
