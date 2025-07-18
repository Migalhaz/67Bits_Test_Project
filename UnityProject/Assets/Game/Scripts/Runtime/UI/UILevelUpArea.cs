using Game.Player;
using UnityEngine;

namespace Game
{
    public class UILevelUpArea : MonoBehaviour
    {
        [Header("Canvas Objects")]
        [SerializeField] UnityEngine.UI.Image m_fillImage;
        [SerializeField] TMPro.TextMeshProUGUI m_priceText;

        [Header("Components")]
        [SerializeField] PlayerLevelUpArea m_levelUpArea;

        private void Update()
        {
            UpdateUI();
        }

        void UpdateUI()
        {
            m_fillImage.fillAmount = (float)m_levelUpArea.m_CurrentPay/m_levelUpArea.m_CurrentPrice;
            m_priceText.text = $"${m_levelUpArea.m_CurrentPay}/{m_levelUpArea.m_CurrentPrice}";
        }
    }
}
