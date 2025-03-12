using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour
{
    [SerializeField]
    float _throtleStrenth = 100f;
    [SerializeField]
    List<WheelCollider> _throtleWheels;
    [SerializeField]
    float _steerAngle = 35f;
    [SerializeField]
    List<WheelCollider> _steerWheels;
    [SerializeField]
    float _brakeTorque = 1000f;
    [SerializeField]
    List<Transform> _wheelMeshes;
    [SerializeField]
    Rigidbody _carRB;
    [SerializeField]
    Transform _cm;

    private void FixedUpdate()
    {
        float throtle = Input.GetAxis("Vertical");
        float steer = Input.GetAxis("Horizontal");
        _carRB.centerOfMass = transform.InverseTransformPoint(_cm.position);

        int i = 0;
        foreach( WheelCollider wheels in _throtleWheels)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                wheels.brakeTorque = _brakeTorque;
            }
            else
            {
                wheels.brakeTorque = 0f;
            }

            wheels.motorTorque = _throtleStrenth * throtle;

            var wheelPos = transform.position;
            var wheelRot = transform.rotation;
            wheels.GetWorldPose(out wheelPos, out wheelRot);
            _wheelMeshes[i].position = wheelPos;
            _wheelMeshes[i].rotation = wheelRot;
            i++;

        }

        _steerWheels[0].steerAngle = _steerAngle * steer;
        _steerWheels[1].steerAngle = _steerAngle * steer;
    }
}
