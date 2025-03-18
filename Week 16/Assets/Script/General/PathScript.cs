using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PathScript : MonoBehaviour
{
    public Color lineColor;
    private List<Transform> nodes;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = lineColor;

        Transform[] pathTransform = GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for ( int i = 0; i < pathTransform.Length; i++)
        {
            if (pathTransform[i] != transform)
            {
                nodes.Add(pathTransform[i]);
            }
        }

        Gizmos.DrawLine(nodes[0].position, nodes[nodes.Count - 1].position);
        Gizmos.DrawWireSphere(nodes[0].position, 0.3f);

        for ( int i = 1; i < nodes.Count; i++)
        {
            Vector3 currentNode = nodes[i].position;
            Vector3 prevNode = nodes[i - 1].position;

            Gizmos.DrawLine(prevNode, currentNode);
            Gizmos.DrawWireSphere(currentNode, 0.3f);
        }
    }

}
