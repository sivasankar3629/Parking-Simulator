using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TrainingLogic : MonoBehaviour
{
    [SerializeField]
    GameObject _startEngineText, _throttleText, _brakeText, _steeringText, _tutorialCompleteText;
    [SerializeField]
    GameObject _brakeCollider, _steerCollider, _finishCollider;
    [SerializeField]
    Volume _postProcessVolume;
    MotionBlur _motionBlur;


    private void Start()
    {
        _postProcessVolume.profile.TryGet(out _motionBlur);
        _motionBlur.intensity.value = 0.5f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _startEngineText.SetActive(false);
            _throttleText.SetActive(true);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _brakeCollider)
        {
            _throttleText.SetActive(false);
            _brakeText.SetActive(true);
            StartCoroutine(SlowMotion(KeyCode.Space));

        }

        if (other.gameObject == _steerCollider)
        {
            //StartCoroutine(SlowMotion(KeyCode.Space));
            _steeringText.SetActive(false);
            _tutorialCompleteText.SetActive(true);
        }

        if (other.gameObject == _finishCollider)
        {
            StartCoroutine(CrossFade.Instance.LoadLevel(2));
            Debug.Log("Tutorial Complete");
        }
    }

    IEnumerator SlowMotion(KeyCode key)
    {
        while (!Input.GetKeyDown(key))
        {
            _motionBlur.intensity.value = 100f;
            Time.timeScale = 0.1f;
            Time.fixedDeltaTime = 0.01f * Time.timeScale;
            yield return null;
        }
        Time.timeScale = 1f;
        _motionBlur.intensity.value = 0.5f;
        _brakeText.SetActive(false);
        _steeringText.SetActive(true);

    }
}
