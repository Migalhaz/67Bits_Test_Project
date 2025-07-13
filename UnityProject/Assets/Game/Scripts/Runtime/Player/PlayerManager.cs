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
        }

        private void Reset()
        {
            GetPlayerComponents();
        }
    }
}
