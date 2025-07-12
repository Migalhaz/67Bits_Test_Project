using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Player
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerInputListener : MonoBehaviour
    {
        public void OnMoveInput(InputAction.CallbackContext context)
        {
            PlayerInputObserverManager.MoveInput(context);
        }
    }
}
