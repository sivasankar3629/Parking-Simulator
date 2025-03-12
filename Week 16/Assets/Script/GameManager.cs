using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject _pauseMenuPanel;
    [SerializeField]
    GameObject _gameOverPanel;
    public static GameManager Instance;
    public bool gameOver;

    private void Awake()
    {
        Instance = this;
        gameOver = false;
    }

    public void PauseGame()
    {
        _pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GameOver()
    {
        gameOver = true;
        Time.timeScale = 0f;
        _gameOverPanel.SetActive(true);
    }

    public void ResumeGame()
    {
        _pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        _pauseMenuPanel.SetActive(false);
        StartCoroutine(CrossFade.Instance.LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    public void HomePage()
    {
        Time.timeScale = 1f;
        StartCoroutine(CrossFade.Instance.LoadLevel(0));
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        StartCoroutine(CrossFade.Instance.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadLevel(int level)
    {
        Time.timeScale = 1f;
        StartCoroutine(CrossFade.Instance.LoadLevel(level + 1));
    }
}
