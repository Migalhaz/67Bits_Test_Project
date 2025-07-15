using Game.Player;
using TMPro;
using UnityEngine;

namespace Game.HUD
{
    public class MoneyCounter : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI m_moneyText;

        private void OnEnable()
        {
            PlayerScoreObserverManager.m_OnPlayerMoneyUpdate += UpdateMoneyText;
        }

        private void OnDisable()
        {
            PlayerScoreObserverManager.m_OnPlayerMoneyUpdate -= UpdateMoneyText;
        }

        void UpdateMoneyText(int moneyValue)
        {
            m_moneyText.text = $"{moneyValue}";
        }
    }
}
