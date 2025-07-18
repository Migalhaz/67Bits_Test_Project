using UnityEngine;

namespace Game.Player
{
    public class PlayerBodyPriceLevelUpArea : PlayerLevelUpArea
    {
        protected override void LevelUp()
        {
            base.LevelUp();
            PlayerScoreObserverManager.BodyPriceLevelUp(m_CurrentAreaLevel);
        }
    }
}
