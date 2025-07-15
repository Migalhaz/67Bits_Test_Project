using UnityEngine;

namespace Game.Player
{
    public class PlayerScore : MonoBehaviour
    {
        int m_stackCount;
        int m_popCount;
        int m_currentMoney;

        private void Awake()
        {
            m_stackCount = 0;
            m_popCount = 0;
            m_currentMoney = 0;
        }

        private void Start()
        {
            PlayerScoreObserverManager.UpdateStackCount(m_stackCount);
            PlayerScoreObserverManager.UpdatePopCount(m_popCount);
            PlayerScoreObserverManager.UpdateMoney(m_currentMoney);
        }

        private void OnEnable()
        {
            PlayerStackerObserveManager.m_OnEnterStack += IncreaseStackCount;
            PlayerStackerObserveManager.m_OnExitStack += IncreasePopCount;
            PlayerStackerObserveManager.m_OnExitStack += IncreaseMoney;

            PlayerScoreObserverManager.m_OnRequestMoneyValue += GetCurrentMoney;
            PlayerScoreObserverManager.m_OnRequestChangeMoneyValue += UpdateMoneyValue;
        }
        private void OnDisable()
        {
            PlayerStackerObserveManager.m_OnEnterStack -= IncreaseStackCount;
            PlayerStackerObserveManager.m_OnExitStack -= IncreasePopCount;
            PlayerStackerObserveManager.m_OnExitStack -= IncreaseMoney;

            PlayerScoreObserverManager.m_OnRequestMoneyValue -= GetCurrentMoney;
            PlayerScoreObserverManager.m_OnRequestChangeMoneyValue -= UpdateMoneyValue;
        }

        void IncreaseStackCount(Transform _)
        {
            ++m_stackCount;
            PlayerScoreObserverManager.UpdateStackCount(m_stackCount);
        }
        
        void IncreasePopCount(Transform _)
        {
            ++m_popCount;
            PlayerScoreObserverManager.UpdatePopCount(m_popCount);
        }

        void IncreaseMoney(Transform _)
        {
            UpdateMoneyValue(m_currentMoney + m_popCount);
        }

        public int GetCurrentMoney() => m_currentMoney;
        public void UpdateMoneyValue(int newValue)
        {
            Debug.Log($"Updating money {newValue}");
            m_currentMoney = newValue;
            PlayerScoreObserverManager.UpdateMoney(m_currentMoney);
        }
        
    }
}
