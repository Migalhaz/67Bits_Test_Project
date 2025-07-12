using UnityEngine;

namespace Game.Player
{
    public class PlayerMove : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] Rigidbody m_rig;

        [Header("Settings")]
        [SerializeField, Min(0)] float m_moveSpeed = 10;
        [SerializeField, Min(0)] float m_rotationSpeed = 10;
        Vector3 m_moveDirection;

        private void OnEnable()
        {
            PlayerInputObserverManager.m_OnMoveInput += MoveInput;
        }

        private void OnDisable()
        {
            PlayerInputObserverManager.m_OnMoveInput -= MoveInput;
        }

        private void Start()
        {
            m_rig ??= PlayerManager.m_Instance.m_Rigidbody;
        }

        void MoveInput(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            Vector2 moveDirection = context.ReadValue<Vector2>();
            Vector3 result = new Vector3(m_moveSpeed * moveDirection.x, 0, m_moveSpeed * moveDirection.y);
            m_moveDirection = result;
        }

        private void FixedUpdate()
        {
            Move();
            UpdateRotation();
        }

        void Move()
        {
            m_rig.AddForce(m_moveDirection, ForceMode.Force);
        }

        void UpdateRotation()
        {
            if (m_moveDirection.sqrMagnitude == 0)
            {
                return;
            }

            float currentAngle = m_rig.rotation.eulerAngles.y;
            const float ANGLEOFFSET = 90;
            float moveAngle = ANGLEOFFSET + Mathf.Atan2(-m_moveDirection.z, m_moveDirection.x) * Mathf.Rad2Deg;
            float finalYAngle = Mathf.LerpAngle(currentAngle, moveAngle, m_rotationSpeed * Time.deltaTime);

            Quaternion result = Quaternion.Euler(0, finalYAngle, 0);
            m_rig.MoveRotation(result);
            //m_rig.transform.rotation = result;
        }

        private void Reset()
        {
            if (!m_rig)
            {
                TryGetComponent(out m_rig);
            }
        }
    }
}
