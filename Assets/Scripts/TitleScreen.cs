using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private UIPanel creditsPanel;

    void Awake()
    {
        if (creditsPanel != null)
        {
            creditsPanel.Close();
        }
    }
    public void PlayGame(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void Credits()
    {
        if (creditsPanel != null)
        {
            creditsPanel.Open();
        }
    }
}
