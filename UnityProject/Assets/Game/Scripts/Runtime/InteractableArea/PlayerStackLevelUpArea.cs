using UnityEngine;

namespace Game.Player
{
    public class PlayerStackLevelUpArea : PlayerLevelUpArea
    {
        protected override void LevelUp()
        {
            base.LevelUp();
            PlayerScoreObserverManager.StackLevelUp(m_CurrentAreaLevel);
        }
    }
}
