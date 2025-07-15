using UnityEngine;

namespace Game
{
    public class PlayerFollower : MonoBehaviour
    {
        Transform m_playerTransform;
        [SerializeField] Vector3 m_positionOffset;
        //[SerializeField] Vector3 m_eulerAnglesOffset;
        [SerializeField, Min(0)] float m_moveSpeed;
        //[SerializeField, Min(0)] float m_rotationSpeed;


        private void Start()
        {
            m_playerTransform = Player.PlayerManager.m_Instance?.transform;
        }

        private void Update()
        {
            Move();
        }

        void Move()
        {
            Vector3 targetPoint = m_playerTransform.position + m_positionOffset;
            Vector3 currentPoint = transform.position;

            Vector3 finalPosition = Vector3.Lerp(currentPoint, targetPoint, m_moveSpeed * Time.deltaTime);
            transform.position = finalPosition;
        }
    }
}
