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

        private void OnEnable()
        {
            PlayerStackerObserveManager.m_OnEnterStack += IncreaseStackCount;
            PlayerStackerObserveManager.m_OnExitStack += IncreasePopCount;
            PlayerStackerObserveManager.m_OnExitStack += UpdateMoney;
        }
        private void OnDisable()
        {
            PlayerStackerObserveManager.m_OnEnterStack -= IncreaseStackCount;
            PlayerStackerObserveManager.m_OnExitStack -= IncreasePopCount;
            PlayerStackerObserveManager.m_OnExitStack -= UpdateMoney;
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

        void UpdateMoney(Transform _)
        {
            m_currentMoney += 1 * m_popCount;
            PlayerScoreObserverManager.UpdateMoney(m_currentMoney);
        }
    }
}
