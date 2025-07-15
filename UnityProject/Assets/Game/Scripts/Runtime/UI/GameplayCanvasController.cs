using Game.Player;
using TMPro;
using UnityEngine;

namespace Game.HUD
{
    public class GameplayCanvasController : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI m_moneyText;
        [SerializeField] TextMeshProUGUI m_stackCountText;
        [SerializeField] TextMeshProUGUI m_popCountText;

        int m_stackCount;

        private void OnEnable()
        {
            PlayerScoreObserverManager.m_OnPlayerMoneyUpdate += UpdateMoneyText;
            PlayerScoreObserverManager.m_OnPlayerPopCountUpdate += UpdatePopText;

            PlayerStackerObserveManager.m_OnEnterStack += IncreaseStackCount;
            PlayerStackerObserveManager.m_OnExitStack += DecreaseStackCount;
        }

        private void OnDisable()
        {
            PlayerScoreObserverManager.m_OnPlayerMoneyUpdate -= UpdateMoneyText;
            PlayerScoreObserverManager.m_OnPlayerPopCountUpdate -= UpdatePopText;

            PlayerStackerObserveManager.m_OnEnterStack -= IncreaseStackCount;
            PlayerStackerObserveManager.m_OnExitStack -= DecreaseStackCount;
        }

        private void Awake()
        {
            m_stackCount = 0;
            UpdateStackText();
        }

        void UpdateMoneyText(int moneyValue)
        {
            m_moneyText.text = $"Money: {moneyValue}";
        }

        void UpdatePopText(int popCount)
        {
            m_popCountText.text = $"Pop: {popCount}";
        }

        void UpdateStackText()
        {
            m_stackCountText.text = $"Stack: {m_stackCount}";
        }

        void IncreaseStackCount(Transform _)
        {
            ++m_stackCount;
            UpdateStackText();
        }

        void DecreaseStackCount(Transform _)
        {
            --m_stackCount;
            UpdateStackText();
        }
    }
}
