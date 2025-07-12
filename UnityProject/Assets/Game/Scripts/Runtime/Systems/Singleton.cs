using UnityEngine;

namespace Game
{
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        static T m_instance;
        public static T m_Instance => m_instance;

        protected virtual void Awake()
        {
            if (m_instance != null && m_instance != this)
            {
                Destroy(gameObject);
                return;
            }
            m_instance = this as T;
        }
    }
}
