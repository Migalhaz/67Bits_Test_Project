using Game.Player;
using UnityEngine;

namespace Game
{
    public abstract class InteractableArea : MonoBehaviour
    {
        [SerializeField, Min(0)] Vector3 m_colliderSize;
        [SerializeField] Vector3 m_colliderOffset;
        [SerializeField] LayerMask m_interactableObjectLayerMask;
        [SerializeField] float m_timeToInteract;
        float m_currentTime;
        Vector3 m_colliderCenter => transform.TransformPoint(m_colliderOffset);

        protected virtual void Awake()
        {
            m_currentTime = m_timeToInteract;
        }

        private void FixedUpdate()
        {
            CheckCollision();
        }

        void CheckCollision()
        {
            bool inCollider = Physics.CheckBox(m_colliderCenter, m_colliderSize, Quaternion.identity, m_interactableObjectLayerMask);

            if (!inCollider)
            {
                m_currentTime = m_timeToInteract;
                return;
            }

            m_currentTime -= Time.fixedDeltaTime;
            if (m_currentTime <= 0)
            {
                m_currentTime = m_timeToInteract;
                Interact();
            }
        }

        protected abstract void Interact();

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireCube(m_colliderCenter, m_colliderSize * 2);
        }
    }
}
