using UnityEngine;

namespace Game.Player
{
    public class PlayerSellArea : InteractableArea
    {
        protected override void Interact()
        {
            PlayerStackerObserveManager.RequestPopStack();
        }
    }
}
