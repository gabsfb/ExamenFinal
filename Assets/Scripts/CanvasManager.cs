using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private UIPanel pausePanel;
    [SerializeField] private UIPanel gameOverPanel;
    [SerializeField] private float timeScale = 0;
    void Awake()
    {
        if (pausePanel != null)
        {
            pausePanel.Close();
        }
        if (gameOverPanel != null)
        {
            gameOverPanel.Close();
        }
    }
    void Update()
    {
        timeScale = Time.timeScale;
        if (Input.GetKeyDown(KeyCode.P))
        {
            SetPause(!pausePanel.gameObject.activeSelf);
        }
    }

    public void SetPause(bool _enable)
    {
        if (_enable)
        {
            Time.timeScale = 0f;
            if (pausePanel != null)
                pausePanel.Open();
        }
        else
        {
            Time.timeScale = 1f;
            if (pausePanel != null)
                pausePanel.Close();
        }
    }

    public void ChangeScene(int index)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(index);
    }

    public void PlayAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public bool IsPaused()
    {
        return pausePanel != null && pausePanel.gameObject.activeSelf;
    }



}
