using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.HUD
{
    public class MenuController : MonoBehaviour
    {
        [Header("UI Components")]
        [SerializeField] UnityEngine.UI.Button m_playButton;
        [SerializeField] UnityEngine.UI.Button m_exitButton;

        [SerializeField, Min(0)] int m_playSceneIndex;

        private void OnEnable()
        {
            m_playButton.onClick.AddListener(OnClickPlay);
            m_exitButton.onClick.AddListener(OnClickExit);
        }

        private void OnDisable()
        {
            m_playButton.onClick.RemoveListener(OnClickPlay);
            m_exitButton.onClick.RemoveListener(OnClickExit);
        }

        void OnClickPlay() => SceneManager.LoadScene(m_playSceneIndex, LoadSceneMode.Single);
        void OnClickExit()
        {

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif

            Application.Quit();

        }
    }
}
