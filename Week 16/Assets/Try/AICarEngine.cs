using System;
using System.Collections.Generic;
using UnityEngine;

public class AICarEngine : MonoBehaviour
{
    [SerializeField]
    Transform _path;
    [SerializeField]
    WheelCollider _wheelFL;
    [SerializeField]
    WheelCollider _wheelFR;
    [SerializeField]
    List<WheelCollider> _wheelColliders;
    [SerializeField]
    List<MeshRenderer> _wheelMeshes;

    List<Transform> nodes;
    [SerializeField]
    int _currentNode = 0;
    [SerializeField]
    int _maxSteerAngle = 40;
    [SerializeField]
    int _maxMotorTorque = 100;
    [SerializeField]
    CarAIBehaviour _carBehavior;
    [SerializeField]
    float _maxSpeed = 15;
    [SerializeField]
    float speed;

    private void Start()
    {
        Transform[] pathTransforms = _path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != _path.transform)
            {
                nodes.Add(pathTransforms[i]);
            }
        }
    }

    private void FixedUpdate()
    {
        ApplySteer();
        ApplyThrotle();
        UpdateWayPoint();
        UpdateWheelMeshes();
        AvoidObstacles();
    }

    private void AvoidObstacles()
    {
        if ( _carBehavior.IsAvoiding())
        {
            _wheelFL.steerAngle = _maxSteerAngle * _carBehavior.AvoidMultiplier();
            _wheelFR.steerAngle = _maxSteerAngle * _carBehavior.AvoidMultiplier();
        }
    }

    private void UpdateWheelMeshes()
    {
        int i = 0;
        foreach (WheelCollider wheels in _wheelColliders)
        {
            Vector3 pos;
            Quaternion rot;

            wheels.GetWorldPose(out pos, out rot);
            _wheelMeshes[i].transform.rotation = rot;
            _wheelMeshes[i].transform.position = pos;
            i++;
        }
    }

    private void UpdateWayPoint()
    {
        if (Vector3.Distance(transform.position, nodes[_currentNode].position) < 8)
        {
            if (_currentNode == nodes.Count - 1) _currentNode = 0;
            else _currentNode++;
        }
    }

    private void ApplyThrotle()
    {
        speed = (2 * Mathf.PI * _wheelFL.radius * _wheelFL.rpm * 60) / 1000;
        speed = MathF.Floor(speed);
        if (speed < _maxSpeed)
        {
            _wheelFR.motorTorque = _maxMotorTorque;
            _wheelFL.motorTorque = _maxMotorTorque;
        }
        else
        {
            _wheelFL.motorTorque = 0f;
            _wheelFR.motorTorque = 0f;
        }

    }

    private void ApplySteer()
    {
        if (!_carBehavior.IsAvoiding())
        {
            Vector3 relativeVector = transform.InverseTransformPoint(nodes[_currentNode].position);
            float newSteer = (relativeVector.x / relativeVector.magnitude) * _maxSteerAngle;
            _wheelFL.steerAngle = newSteer;
            _wheelFR.steerAngle = newSteer;
        }
    }


}
