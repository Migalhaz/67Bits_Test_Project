using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Player
{
    public static class PlayerInputObserverManager
    {
        public static System.Action<Vector3> m_OnMoveInput = null;

        public static void MoveInput(Vector3 inputDirection) => m_OnMoveInput?.Invoke(inputDirection);
    }

    public static class PlayerAttackObserverManager
    {
        public static System.Action<Collider> m_OnEnterAttackCollision = null;
        public static System.Action<Collider> m_OnExitAttackCollision = null;
        public static void EnterAttackCollision(Collider enemyCollider) => m_OnEnterAttackCollision?.Invoke(enemyCollider);
        public static void ExitAttackCollision(Collider enemyCollider) => m_OnExitAttackCollision?.Invoke(enemyCollider);
    }

    public static class PlayerStackerObserveManager
    {
        public static System.Action<Transform> m_OnRequestStack = null;
        public static System.Action<Transform> m_OnEnterStack = null;

        public static System.Action m_OnRequestPopStack = null;
        public static System.Action<Transform> m_OnExitStack = null;

        public static System.Func<int> m_OnRequestStackMaxCount = null;
        public static System.Func<int> m_OnRequestStackCount = null;

        public static System.Action<int> m_OnUpdateMaxStackCount = null;

        public static void RequestStack(Transform objectTransform) => m_OnRequestStack?.Invoke(objectTransform);
        public static void EnterStack(Transform objectTransform) => m_OnEnterStack?.Invoke(objectTransform);

        public static void RequestPopStack() => m_OnRequestPopStack?.Invoke();
        public static void ExitStack(Transform transform) => m_OnExitStack?.Invoke(transform);

        public static int? RequestStackMaxCount() => m_OnRequestStackMaxCount?.Invoke();
        public static int? RequestStackCount() => m_OnRequestStackCount?.Invoke();

        public static void UpdateMaxStackCount(int maxStackCount) => m_OnUpdateMaxStackCount?.Invoke(maxStackCount);
    }

    public static class PlayerScoreObserverManager
    {
        public static System.Action<int> m_OnPlayerStackCountUpdate = null;
        public static System.Action<int> m_OnPlayerPopCountUpdate = null;
        public static System.Action<int> m_OnPlayerMoneyUpdate = null;

        public static System.Func<int> m_OnRequestMoneyValue = null;
        public static System.Action<int> m_OnRequestChangeMoneyValue = null;

        public static System.Action<int> m_OnStackLevelUp = null;
        public static System.Action<int> m_OnBodyPriceLevelUp = null;

        public static void UpdateStackCount(int newStackCount) => m_OnPlayerStackCountUpdate?.Invoke(newStackCount);
        public static void UpdatePopCount(int newPopCount) => m_OnPlayerPopCountUpdate?.Invoke(newPopCount);
        public static void UpdateMoney(int newMoney) => m_OnPlayerMoneyUpdate?.Invoke(newMoney);

        public static int? RequestMoney() => m_OnRequestMoneyValue?.Invoke();
        public static void RequestChangeMoneyValue(int newValue) => m_OnRequestChangeMoneyValue?.Invoke(newValue);

        public static void StackLevelUp(int newLevel) => m_OnStackLevelUp?.Invoke(newLevel);
        public static void BodyPriceLevelUp(int newLevel) => m_OnBodyPriceLevelUp?.Invoke(newLevel);
    }
}
