using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Player
{
    public static class PlayerInputObserverManager
    {
        public static System.Action<InputAction.CallbackContext> m_OnMoveInput = null;

        public static void MoveInput(InputAction.CallbackContext context) => m_OnMoveInput?.Invoke(context);
    }
}
