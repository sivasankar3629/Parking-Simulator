using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarController : MonoBehaviour
{
    float _throttle;
    float _steer;
    [SerializeField] float _throttleStrength = 100;
    [SerializeField] float _breakStrength = 2000f;
    [SerializeField] float _maxTurn = 25f;
    [SerializeField] float _maxVelocity = 15f;
    [SerializeField] float _vehVelocity;
    [SerializeField] float _rbVelocity;

    [SerializeField] List<WheelCollider> _throttleWheel;
    [SerializeField] List<WheelCollider> _steerWheel;
    [SerializeField] List<GameObject> _wheelMesh;
    [SerializeField] List<WheelCollider> _idleWheelCollider;
    [SerializeField] List<GameObject> _idleWheelMeshes;

    bool _isEngineOn = false;
    [SerializeField] Transform _CM;
    [SerializeField] Rigidbody _carRb;
    [SerializeField] LayerMask _playerBoundary;

    private void Start()
    {
        _carRb.centerOfMass = transform.InverseTransformPoint(_CM.position);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(StartEngine());
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        if (_isEngineOn)
        {
            _throttle = Input.GetAxis("Vertical");
        }
        
        _steer = Input.GetAxis("Horizontal");

        for (int i = 0; i < _throttleWheel.Count; i++)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                _throttleWheel[i].brakeTorque = _breakStrength;
            }
            else
            {
                _throttleWheel[i].brakeTorque = 0f;
            }

            // Throttle
            _throttleWheel[i].motorTorque = _throttle * _throttleStrength;
            _vehVelocity = (2 * Mathf.PI * _throttleWheel[i].radius * _throttleWheel[i].rpm * 60) / 1000;
            _vehVelocity = MathF.Floor(_vehVelocity);
            if (_vehVelocity < _maxVelocity)
            {
                _throttleWheel[i].motorTorque = _throttleStrength * _throttle;
            }
            else
            {
                _throttleWheel[i].motorTorque = 0f;
            }

            // Steering
            _steerWheel[0].steerAngle = _steer * _maxTurn;
            _steerWheel[1].steerAngle = _steer * _maxTurn;

            // Animation
            var pos = transform.position;
            var rot = transform.rotation;
            _throttleWheel[i].GetWorldPose(out pos, out rot);
            _wheelMesh[i].transform.position = pos;
            _wheelMesh[i].transform.rotation = rot;

            _rbVelocity = Mathf.Abs(_carRb.linearVelocity.magnitude);
        }

        for ( int i = 0; i < _idleWheelCollider.Count; i++)
        {
            var pos = transform.position;
            var rot = transform.rotation;
            _idleWheelCollider[i].GetWorldPose(out pos, out rot);
            _idleWheelMeshes[i].transform.position = pos;
            _idleWheelMeshes[i].transform.rotation = rot;
        }
    }
    
    IEnumerator StartEngine()
    {
        if (!_isEngineOn)
        {
            yield return new WaitForSeconds(1f);
            _isEngineOn = true;
        }
        else
        {
            _isEngineOn = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("ParkingSpace") && collision.collider.gameObject.layer != _playerBoundary)
        {
            GameManager.Instance.GameLost();
            Debug.Log(collision.collider.name);
        }
    }

    public float Velocity()
    {
        return _rbVelocity;
    }

}
