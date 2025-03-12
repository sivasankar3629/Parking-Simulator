using UnityEngine;

public class ParkingCheck : MonoBehaviour
{
    Transform _car;
    bool _isParked = false;

    private void Start()
    {
        _car = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            float posDiff = Vector3.Distance( new Vector3(_car.position.x,0,_car.position.z), new Vector3(transform.position.x, 0, transform.position.z));
            float angleDiff = Quaternion.Angle(Quaternion.Euler(_car.rotation.x, 0f, _car.rotation.z) , transform.rotation);

            _isParked = (angleDiff < 10f && posDiff < 0.5f);
            if (_isParked && !GameManager.Instance.gameOver)
            {

                Debug.Log(_car.GetComponent<Rigidbody>().linearVelocity.magnitude);
                GameManager.Instance.GameOver();
            }
        }
    }


}
