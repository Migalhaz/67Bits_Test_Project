using UnityEngine;

namespace Game
{
    public class CamMove : MonoBehaviour
    {
        Transform m_playerTransform;
        [SerializeField] float m_minPlayerZPosition;
        [SerializeField] float m_maxPlayerZPosition;

        [SerializeField] float m_yCamPositionBuffer;
        [SerializeField] float m_minYCamPosition;

        private void Start()
        {
            m_playerTransform = Player.PlayerManager.m_Instance.transform;
        }

        private void Update()
        {
            float x = m_playerTransform.position.x;

            float zInterval = m_maxPlayerZPosition - m_minPlayerZPosition;
            float currentZ = (m_playerTransform.position.z - m_minPlayerZPosition) / zInterval;

            float y = m_minYCamPosition + (currentZ * m_yCamPositionBuffer);

            transform.position = transform.position.With(x: x, y: y);
        }
    }
}
