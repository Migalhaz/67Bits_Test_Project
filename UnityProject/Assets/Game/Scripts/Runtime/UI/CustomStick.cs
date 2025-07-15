using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace Game.HUD
{
    public class CustomStick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField, Min(0)] float m_joystickDistance = 50;
        Vector2 m_direction;

        [Header("UI Components")]
        [SerializeField] Image m_containerImage;
        
        [SerializeField] Image m_stickImage;

        public void OnDrag(PointerEventData eventData)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(m_containerImage.rectTransform, eventData.position, eventData.enterEventCamera, out Vector2 pos))
            {
                pos.x /= m_containerImage.rectTransform.sizeDelta.x;
                pos.y /= m_containerImage.rectTransform.sizeDelta.y;

                Vector2 currentPivot = m_containerImage.rectTransform.pivot;
                pos.x += currentPivot.x - 0.5f;
                pos.y += currentPivot.y - 0.5f;

                float x = Mathf.Clamp(pos.x, -1, 1);
                float y = Mathf.Clamp(pos.y, -1, 1);

                m_direction.Set(x, y);
                m_direction.Normalize();
                Vector2 stickPosition = m_direction * m_joystickDistance;

                m_stickImage.rectTransform.anchoredPosition = stickPosition;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            m_direction = Vector2.zero;
            m_stickImage.rectTransform.anchoredPosition = Vector2.zero;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            m_direction = Vector2.zero;
            m_stickImage.rectTransform.anchoredPosition = Vector2.zero;
        }

        void Update()
        {
            Player.PlayerInputObserverManager.MoveInput(m_direction);
        }
    }
}
