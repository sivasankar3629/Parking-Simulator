using UnityEngine;

public class ParkingCheck : MonoBehaviour
{
    Transform _car;
    bool _isParked = false;
    [SerializeField]
    float _centerOffset = 0;
    [SerializeField]
    bool _onlyReverseParking = false;
    

    private void Start()
    {
        _car = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            float posDiff = Vector3.Distance( new Vector3(_car.position.x,0,_car.position.z), new Vector3(transform.position.x + _centerOffset, 0, transform.position.z));
            float angleDiff = Quaternion.Angle(Quaternion.Euler(0, _car.eulerAngles.y, 0) , Quaternion.Euler(0, transform.eulerAngles.y, 0));
            Debug.Log(posDiff + " " + angleDiff);

            float carVelocity = _car.GetComponent<CarController>().Velocity();

            if (_onlyReverseParking)
            {
                _isParked = (angleDiff > 170f && posDiff < 0.7f &&  carVelocity < 0.2f );
            }
            else
            {
                _isParked = ((angleDiff < 10f || angleDiff > 170f) && posDiff < 0.7f && carVelocity < 0.2f );
            }

            if (_isParked && !GameManager.Instance.gameOver)
            {
                Debug.Log(_car.GetComponent<Rigidbody>().linearVelocity.magnitude);
                GameManager.Instance.GameWon();
            }
        }
    }


}
