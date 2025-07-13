using UnityEngine;

namespace Game.Player
{
    public class PlayerSellArea : MonoBehaviour
    {
        [SerializeField, Min(0)] Vector3 m_colliderSize;
        [SerializeField] Vector3 m_colliderOffset;
        [SerializeField] LayerMask m_playerLayerMask;
        [SerializeField] float m_timeToSell;
        float m_currentTime;
        Vector3 m_colliderCenter => transform.TransformPoint(m_colliderOffset);

        private void Awake()
        {
            m_currentTime = m_timeToSell;
        }

        private void FixedUpdate()
        {
            CheckCollision();
        }

        void CheckCollision()
        {
            bool inCollider = Physics.CheckBox(m_colliderCenter, m_colliderSize, Quaternion.identity, m_playerLayerMask);

            if (!inCollider)
            {
                m_currentTime = m_timeToSell;
                return;    
            }
            
            m_currentTime -= Time.fixedDeltaTime;
            if (m_currentTime <= 0)
            {
                m_currentTime = m_timeToSell;
                PlayerStackerObserveManager.RequestPopStack();
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireCube(m_colliderCenter, m_colliderSize * 2);
        }
    }
}
