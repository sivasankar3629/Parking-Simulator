using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AICarMaker : EditorWindow
{
    [MenuItem("Window/AI Car Maker")]
    static void ShowWindow()
    {
        GetWindow<AICarMaker>("AI Car Maker");
    }

    GameObject car;
    GameObject[] Wheels = new GameObject[4];
    GameObject meshes;
    GameObject colliders;
    string[] meshString = { "Wheel_Mesh_FL", "Wheel_Mesh_FR", "Wheel_Mesh_RL", "Wheel_Mesh_RR" };
    string[] colliderString = { "WheelCollider_FL", "WheelCollider_FR", "WheelCollider_RL", "WheelCollider_RR" };

    private void OnGUI()
    {

        car = (GameObject)EditorGUILayout.ObjectField(car, typeof(GameObject),true);
        
        Wheels[0] = (GameObject)EditorGUILayout.ObjectField(meshString[0], Wheels[0], typeof(GameObject), true);
        Wheels[1] = (GameObject)EditorGUILayout.ObjectField(meshString[1], Wheels[1], typeof(GameObject), true);
        Wheels[2] = (GameObject)EditorGUILayout.ObjectField(meshString[2], Wheels[2], typeof(GameObject), true);
        Wheels[3] = (GameObject)EditorGUILayout.ObjectField(meshString[3], Wheels[3], typeof(GameObject), true);


        if (GUILayout.Button("Add RigidBody"))
        {
            car.AddComponent<Rigidbody>();
            car.GetComponent<Rigidbody>().mass = 1200;
            car.AddComponent<AICarEngine>();
            car.AddComponent<CarAIBehaviour>();
        }

        if (GUILayout.Button("Set Child"))
        {
            meshes = new GameObject("Wheel_Meshes");
            colliders = new GameObject("Wheel_Colliders");
            meshes.transform.SetParent(car.transform);
            colliders.transform.SetParent(car.transform);
        }

        if (GUILayout.Button("Add Meshes and Colliders"))
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject mesh = new GameObject(meshString[i]);
                // Adding Transform
                mesh.transform.SetParent(meshes.transform);
                mesh.transform.position = Wheels[i].transform.position;
                mesh.transform.rotation = Wheels[i].transform.rotation;
                mesh.transform.localScale = Wheels[i].transform.localScale;
                // Adding MeshFilter
                MeshFilter meshFilter = mesh.AddComponent<MeshFilter>();
                meshFilter.sharedMesh = Wheels[i].GetComponent<MeshFilter>().sharedMesh;
                // Adding MeshRenderer
                MeshRenderer meshRenderer = mesh.AddComponent<MeshRenderer>();
                meshRenderer.sharedMaterials = Wheels[i].GetComponent<MeshRenderer>().sharedMaterials;


                GameObject collider = new GameObject(colliderString[i]);
                collider.transform.SetParent(colliders.transform);
                collider.transform.position = Wheels[i].transform.position;
                collider.transform.rotation = Wheels[i].transform.rotation;
                collider.transform.localScale = Wheels[i].transform.localScale;
                // Adding Collider
                WheelCollider wheelCollider = collider.AddComponent<WheelCollider>();
                wheelCollider.radius = Wheels[i].transform.position.y;
                wheelCollider.suspensionDistance = 0.2f;
            }
        }


    }

}
