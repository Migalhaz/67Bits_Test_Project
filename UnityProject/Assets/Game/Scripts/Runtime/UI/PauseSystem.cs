using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.HUD
{
    public class PauseSystem : MonoBehaviour
    {
        [SerializeField] GameObject m_pauseRoot;
        [SerializeField] Button m_pauseButton;
        [SerializeField] Button m_mainMenuButton;
        [SerializeField, Min(0)] int m_mainMenuSceneIndex = 0;
        bool m_isPaused;

        private void Awake()
        {
            SetPause(false);
        }

        private void OnEnable()
        {
            m_pauseButton.onClick.AddListener(TogglePause);
            m_mainMenuButton.onClick.AddListener(OnClickMainMenu);
        }

        private void OnDisable()
        {
            m_pauseButton.onClick.RemoveListener(TogglePause);
            m_mainMenuButton.onClick.RemoveListener(OnClickMainMenu);
        }

        void TogglePause() => SetPause(!m_isPaused);

        void SetPause(bool isPause)
        {
            m_isPaused = isPause;
            m_pauseRoot.SetActive(isPause);

            const float PAUSETIMESCALE = 0;
            const float GAMERUNTIMESCALE = 1;

            float timeScale = m_isPaused ? PAUSETIMESCALE : GAMERUNTIMESCALE;
            Time.timeScale = timeScale;
        }

        void OnClickMainMenu() => SceneManager.LoadScene(m_mainMenuSceneIndex, LoadSceneMode.Single);
    }
}
