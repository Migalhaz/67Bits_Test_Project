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
        bool m_inCollider;
        Vector3 m_colliderCenter => transform.TransformPoint(m_colliderOffset);

        protected virtual void Awake()
        {
            m_currentTime = m_timeToInteract;
        }

        private void Update()
        {
            if (!m_inCollider)
            {
                OutCollider();
                return;
            }

            InCollider();
        }

        protected virtual void InCollider()
        {
            m_currentTime -= Time.deltaTime;
            if (m_currentTime <= 0)
            {
                m_currentTime = m_timeToInteract;
                Interact();
            }
        }

        protected virtual void OutCollider()
        {
            m_currentTime = m_timeToInteract;
        }

        private void FixedUpdate()
        {
            CheckCollision();
        }

        void CheckCollision()
        {
            m_inCollider = Physics.CheckBox(m_colliderCenter, m_colliderSize, Quaternion.identity, m_interactableObjectLayerMask);            
        }

        protected abstract void Interact();

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireCube(m_colliderCenter, m_colliderSize * 2);
        }
    }
}
