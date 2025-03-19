using System.Collections.Generic;
using UnityEngine;

public class PlayerPath : MonoBehaviour
{
    [SerializeField]
    Transform checkpoint;
    List<Transform> _checkpoints;
    public float rotationSpeed = 5f;  // Speed of rotation


    private void Start()
    {
        Transform[] checkpoints = checkpoint.GetComponentsInChildren<Transform>();
        _checkpoints = new List<Transform>();

        for ( int i = 0; i < checkpoints.Length; i++)
        {
            if (checkpoints[i] != checkpoint.transform)
            {
                Debug.Log("--" + checkpoints[i]);
                _checkpoints.Add(checkpoints[i]);
            }            
        }
    }

    void Update()
    {
        if (_checkpoints[0] != null)
        {
            Vector3 direction = _checkpoints[0].position - transform.position;
            direction.y = 90; // Keep arrow level (ignore vertical rotation)


            Quaternion targetRotation = Quaternion.LookRotation(direction);
            targetRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 90);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.gameObject == _checkpoints[0].gameObject)
        {
            _checkpoints.Remove(_checkpoints[0]);
            Destroy(other.gameObject, 0.5f);
        }
    }
}

