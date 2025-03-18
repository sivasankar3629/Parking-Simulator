using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class CrossFade : MonoBehaviour
{
    [SerializeField]
    Animator _transitionAnimator;
    public static CrossFade Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (_transitionAnimator != null)
        {
            _transitionAnimator.SetTrigger("End"); 
        }
    }

    public IEnumerator LoadLevel(int levelIndex)
    {
        _transitionAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(levelIndex);
    }
}
