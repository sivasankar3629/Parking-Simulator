using System.Collections.Generic;
using UnityEngine;

public class PlayerPath : MonoBehaviour
{
    [SerializeField]
    List<Transform> _checkpoints;
    public float rotationSpeed = 5f;  // Speed of rotation

    void Update()
    {
        if (_checkpoints[0] != null)
        {
            Vector3 direction = _checkpoints[0].position - transform.position;
            //direction.x = 70; // Keep arrow level (ignore vertical rotation)
            //direction.z = 90;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
        Debug.Log(_checkpoints[0].name);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.gameObject == _checkpoints[0].gameObject)
        {
            _checkpoints.Remove(_checkpoints[0]);
            Destroy(other.gameObject);
        }
    }
}

