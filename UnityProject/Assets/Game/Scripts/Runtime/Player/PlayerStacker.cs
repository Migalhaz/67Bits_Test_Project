using UnityEngine;

namespace Game.Player
{
    public class PlayerStacker : MonoBehaviour
    {
        [SerializeField, Min(1)] int m_stackMaxCount = 1;
        [SerializeField] Transform m_startPoint;
        [SerializeField] System.Collections.Generic.List<Transform> m_points;
        [SerializeField, Min(0)] float m_pileMoveSpeed;
        [SerializeField, Min(0)] float m_pileRotationSpeed;
        [SerializeField] float m_offset;

        private void OnEnable()
        {
            PlayerStackerObserveManager.m_OnRequestStack += StackNewObject;
            PlayerStackerObserveManager.m_OnRequestPopStack += PopStack;
        }

        private void OnDisable()
        {
            PlayerStackerObserveManager.m_OnRequestStack -= StackNewObject;
            PlayerStackerObserveManager.m_OnRequestPopStack -= PopStack;

        }

        private void Start()
        {
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
                UpdatePosition(currentPoint, lastPoint);
                //UpdateRotation(currentPoint, lastPoint);
            }

            void UpdatePosition(Transform currentPoint, Transform lastPoint)
            {
                Vector3 currentPosition = currentPoint.position;
                Vector3 lastPosition = lastPoint.position;
                lastPosition.y += m_offset;

                currentPoint.position = Vector3.Lerp(currentPosition, lastPosition, m_pileMoveSpeed * Time.deltaTime);
            }

            void UpdateRotation(Transform currentPoint, Transform lastPoint)
            {
                Quaternion currentRotation = currentPoint.rotation;
                Quaternion lastRotation = Quaternion.Euler(lastPoint.eulerAngles.With(x: 90, z: 0));
                currentPoint.rotation = Quaternion.Lerp(currentRotation, lastRotation, m_pileRotationSpeed * Time.deltaTime);
            }
        }

        void StackNewObject(Transform objectTransform)
        {
            if (m_points.Count >= m_stackMaxCount+1)
            {
                return;
            }
            PlayerStackerObserveManager.EnterStack(objectTransform);
            m_points.Add(objectTransform);
        }

        void PopStack()
        {
            int count = m_points.Count;
            if (count > 1)
            {
                Debug.Log("Popping");
                m_points.RemoveAt(count-1);
            }
        }
    }
}
