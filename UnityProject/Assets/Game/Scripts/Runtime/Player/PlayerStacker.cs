using UnityEngine;

namespace Game.Player
{
    public class PlayerStacker : MonoBehaviour
    {
        [SerializeField, Min(1)] int m_startStackMaxCount = 1;
        [SerializeField, Min(1)] int m_stackLevelMultiplier = 2;
        int m_currentStackLevel = 0;

        public int m_CurrentStackMaxCount => m_startStackMaxCount + (m_currentStackLevel * m_stackLevelMultiplier);

        [SerializeField] Transform m_startPoint;
        System.Collections.Generic.List<Transform> m_points;
        [SerializeField, Min(0)] float m_pileMoveSpeed;
        [SerializeField] float m_offset;

        Vector3 m_moveDirection;

        public int GetCurrentStackMaxCount() => m_CurrentStackMaxCount;
        public int GetCurrentStackCount() => m_points.Count-1;

        private void OnEnable()
        {
            PlayerStackerObserveManager.m_OnRequestStack += StackNewObject;
            PlayerStackerObserveManager.m_OnRequestPopStack += PopStack;
            PlayerInputObserverManager.m_OnMoveInput += SetMoveDirection;
            PlayerStackerObserveManager.m_OnRequestStackMaxCount += GetCurrentStackMaxCount;
            PlayerStackerObserveManager.m_OnRequestStackCount += GetCurrentStackCount;
            PlayerScoreObserverManager.m_OnStackLevelUp += OnLevelUp;
        }

        private void OnDisable()
        {
            PlayerStackerObserveManager.m_OnRequestStack -= StackNewObject;
            PlayerStackerObserveManager.m_OnRequestPopStack -= PopStack;
            PlayerInputObserverManager.m_OnMoveInput -= SetMoveDirection;
            PlayerStackerObserveManager.m_OnRequestStackMaxCount -= GetCurrentStackMaxCount;
            PlayerStackerObserveManager.m_OnRequestStackCount -= GetCurrentStackCount;
            PlayerScoreObserverManager.m_OnStackLevelUp += OnLevelUp;
        }

        private void Awake()
        {
            m_currentStackLevel = 0;
            (m_points ??= new()).Clear();
            m_points.Add(m_startPoint);
        }

        private void Update()
        {
            UpdateStackPosition();
        }

        void UpdateStackPosition()
        {
            m_startPoint.transform.eulerAngles = transform.eulerAngles.With(y: transform.eulerAngles.y + 90);

            for (int i = 1; i < m_points.Count; ++i)
            {
                Transform currentPoint = m_points[i];
                Transform lastPoint = m_points[i - 1];

                float height = m_offset - (m_offset * ((float)i / (float)m_points.Count));
                UpdatePosition(currentPoint, lastPoint, height);
            }

            void UpdatePosition(Transform currentPoint, Transform lastPoint, float height)
            {
                Vector3 currentPosition = currentPoint.position;
                Vector3 lastPosition = lastPoint.position;
                lastPosition.y += height;

                currentPoint.position = Vector3.Lerp(currentPosition, lastPosition, m_pileMoveSpeed * Time.deltaTime);
            }
        }

        void SetMoveDirection(Vector3 moveDirection)
        {
            m_moveDirection.Set(moveDirection.x, 0, moveDirection.y);
        }

        void StackNewObject(Transform objectTransform)
        {
            if (m_points.Count >= m_CurrentStackMaxCount+1)
            {
                return;
            }
            m_points.Add(objectTransform);
            PlayerStackerObserveManager.EnterStack(objectTransform);
        }

        void PopStack()
        {
            int count = m_points.Count;
            if (count > 1)
            {
                Transform popObject = m_points[count-1];
                m_points.RemoveAt(count-1);
                PlayerStackerObserveManager.ExitStack(popObject);
            }
        }

        void OnLevelUp(int newPlayerLevel)
        {
            m_currentStackLevel++;
            PlayerStackerObserveManager.UpdateMaxStackCount(m_CurrentStackMaxCount);
        }
    }
}
