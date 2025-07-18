using UnityEngine;

namespace Game
{
    public class CamMove : MonoBehaviour
    {
        Transform m_playerTransform;
        
        [Header("X Settings")]
        [SerializeField] Vector2 m_clampXPosition;
        
        [Header("Y Settings")]
        [SerializeField] float m_yCamPositionBuffer;
        [SerializeField] float m_minYCamPosition;

        [Header("Z Settings")]
        [SerializeField] float m_minPlayerZPosition;
        [SerializeField] float m_maxPlayerZPosition;

        private void Start()
        {
            m_playerTransform = Player.PlayerManager.m_Instance?.transform;
        }

        private void Update()
        {
            if (!m_playerTransform)
            {
                return;
            }
            float x = Mathf.Clamp(m_playerTransform.position.x, m_clampXPosition.x, m_clampXPosition.y);

            float zInterval = m_maxPlayerZPosition - m_minPlayerZPosition;
            float currentZ = (m_playerTransform.position.z - m_minPlayerZPosition) / zInterval;

            float y = m_minYCamPosition + (currentZ * m_yCamPositionBuffer);

            transform.position = transform.position.With(x: x, y: y);
        }
    }
}
