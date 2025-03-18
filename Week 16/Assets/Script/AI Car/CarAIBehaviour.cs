using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarAIBehaviour : MonoBehaviour
{
    [SerializeField]
    Transform _fSensor;
    [SerializeField]
    Transform _frSensor;
    [SerializeField]
    Transform _flSensor;
    [SerializeField]
    int _sensorRange = 5;
    [SerializeField]
    int _sideSensorAngle = 45;
    [SerializeField]
    LayerMask _playerBoundary;

    bool _avoiding;
    float _avoidMultiplier;

    RaycastHit hit;

    private void FixedUpdate()
    {
        SensorDetection();
    }    

    private void SensorDetection()
    {
        _avoiding = false;
        _avoidMultiplier = 0;

        // Front Left Sensor
        RayCast(_flSensor.position, transform.forward, 1, _playerBoundary);

        // Front Left Angle Sensor
        RayCast(_flSensor.position, Quaternion.AngleAxis(-_sideSensorAngle, transform.up) * transform.forward, 0.5f, _playerBoundary);

        // Front Right Sensor
        RayCast(_frSensor.position, transform.forward, -1, _playerBoundary);

        // Front Right Angle Sensor
        RayCast(_frSensor.position, Quaternion.AngleAxis(_sideSensorAngle, transform.up) * transform.forward, -0.5f, _playerBoundary);

        // Front sensor
        if (_avoidMultiplier == 0)
        {
            RayCast(_fSensor.position, transform.forward, 0, _playerBoundary);
        }
        
    }

    private void RayCast(Vector3 origin, Vector3 dir, float avoidMultiplier, LayerMask mask)
    {
        if (Physics.Raycast(origin, dir, out hit, _sensorRange, ~mask))
        {
            if (!hit.collider.CompareTag("Waypoint"))
            {
                Debug.DrawLine(origin, hit.point);
                _avoiding = true;
                if (avoidMultiplier == 0)
                {
                    if (hit.normal.x < 0)
                    {
                        _avoidMultiplier = -1;
                    }
                    else
                    {
                        _avoidMultiplier = 1;
                    }
                }
                _avoidMultiplier += avoidMultiplier;
            }
        }
    }

    public bool IsAvoiding()
    {
        return _avoiding;
    }

    public float AvoidMultiplier()
    {
        return _avoidMultiplier;
    }
}
