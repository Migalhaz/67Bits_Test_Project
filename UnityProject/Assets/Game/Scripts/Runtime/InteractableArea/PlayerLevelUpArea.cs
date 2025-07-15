using UnityEngine;

namespace Game.Player
{
    public class PlayerLevelUpArea : InteractableArea
    {
        [SerializeField, Min(0)] int m_startPrice = 10;
        int m_lastPrice;
        int m_currentPrice;
        int m_levelUpCount;
        int m_currentPay;

        public int m_CurrentPlayerLevel => m_levelUpCount + 1;
        protected override void Awake()
        {
            base.Awake();
            m_currentPay = 0;
            m_levelUpCount = 0;
            m_lastPrice = 0;
            m_currentPrice = m_startPrice;
        }

        protected override void Interact()
        {
            CheckMoney();
        }

        void CheckMoney()
        {
            int currentPlayerMoney = PlayerScoreObserverManager.RequestMoney() ?? 0;
            if (currentPlayerMoney <= 0) return;

            ++m_currentPay;
            PlayerScoreObserverManager.RequestChangeMoneyValue(currentPlayerMoney - 1);

            if (m_currentPay >= m_currentPrice)
            {
                LevelUp();
            }
        }

        void LevelUp()
        {
            ++m_levelUpCount;
            m_currentPay = 0;
            int currentPrice = m_currentPrice;
            m_currentPrice += m_lastPrice;
            m_lastPrice = currentPrice;

            PlayerScoreObserverManager.LevelUp(m_CurrentPlayerLevel);
        }
    }
}
