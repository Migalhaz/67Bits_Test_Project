using Game.Player;
using UnityEngine;

namespace Game.HUD
{
    public class UIStackCounter : MonoBehaviour
    {
        [Header("UI Components")]
        [SerializeField] UnityEngine.UI.Image m_fillImage;
        [SerializeField] TMPro.TextMeshProUGUI m_stackCountText;

        [Header("Settings")]
        int m_maxStackCount;
        int m_currentStackCount;

        void Start()
        {
            OnUpdateStack();
        }

        private void OnEnable()
        {
            PlayerStackerObserveManager.m_OnEnterStack += OnUpdateStack;
            PlayerStackerObserveManager.m_OnExitStack += OnUpdateStack;
            PlayerStackerObserveManager.m_OnUpdateMaxStackCount += OnUpdateStack;
        }

        private void OnDisable()
        {
            PlayerStackerObserveManager.m_OnEnterStack -= OnUpdateStack;
            PlayerStackerObserveManager.m_OnExitStack -= OnUpdateStack;
            PlayerStackerObserveManager.m_OnUpdateMaxStackCount -= OnUpdateStack;
        }

        private void Update()
        {
            m_fillImage.fillAmount = (float)m_currentStackCount / (float)m_maxStackCount;
        }

        void OnUpdateStack()
        {
            m_maxStackCount = PlayerStackerObserveManager.RequestStackMaxCount() ?? 1;
            m_currentStackCount = PlayerStackerObserveManager.RequestStackCount() ?? 0;

            //m_fillImage.fillAmount = (float)m_currentStackCount / (float)m_maxStackCount;
            m_stackCountText.text = $"{m_currentStackCount}/{m_maxStackCount}";
        }

        void OnUpdateStack(Transform _) => OnUpdateStack();
        void OnUpdateStack(int _) => OnUpdateStack();
    }
}
