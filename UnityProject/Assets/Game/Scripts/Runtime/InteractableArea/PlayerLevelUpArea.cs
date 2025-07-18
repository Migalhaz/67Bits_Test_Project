using UnityEngine;

namespace Game.Player
{
    public abstract class PlayerLevelUpArea : InteractableArea
    {
        [SerializeField, Min(0)] int m_startPrice = 10;
        int m_lastPrice;
        int m_currentPrice;
        int m_levelUpCount;
        int m_currentPay;

        public int m_CurrentPrice => m_currentPrice;
        public int m_CurrentPay => m_currentPay;

        [SerializeField, Min(1)] int m_timeToIncreaseBuyValue = 1;

        float m_currentTimeInCollider;

        public int m_CurrentAreaLevel => m_levelUpCount + 1;
        protected override void Awake()
        {
            base.Awake();
            m_currentPay = 0;
            m_levelUpCount = 0;
            m_lastPrice = 0;
            m_currentPrice = m_startPrice;
        }

        protected override void OutCollider()
        {
            base.OutCollider();
            m_currentTimeInCollider = 0;
        }

        protected override void InCollider()
        {
            base.InCollider();
            m_currentTimeInCollider += Time.deltaTime;
        }

        protected override void Interact()
        {
            CheckMoney();
        }

        void CheckMoney()
        {
            int currentPlayerMoney = PlayerScoreObserverManager.RequestMoney() ?? 0;


            if (currentPlayerMoney <= 0) return;

            int pay = Mathf.FloorToInt(m_currentTimeInCollider / m_timeToIncreaseBuyValue);

            pay = Mathf.Clamp(pay, 1, currentPlayerMoney);

            m_currentPay += pay;
            PlayerScoreObserverManager.RequestChangeMoneyValue(currentPlayerMoney - pay);

            if (m_currentPay >= m_currentPrice)
            {
                LevelUp();
            }
        }

        protected virtual void LevelUp()
        {
            ++m_levelUpCount;
            m_currentPay = 0;
            int currentPrice = m_currentPrice;
            m_currentPrice += (m_lastPrice/2);
            m_lastPrice = currentPrice;
        }
    }
}
