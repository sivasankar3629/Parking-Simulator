using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    GameObject _pauseMenuPanel;

    public void PauseGame()
    {
        _pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
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
        StartCoroutine(TransitionAnimation(SceneManager.GetActiveScene().buildIndex));
    }

    public void HomePage()
    {
        Time.timeScale = 1f;
        StartCoroutine(TransitionAnimation(0));
    }

    IEnumerator TransitionAnimation(int scene)
    {
       // _animator.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(scene);

    }
}
