using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDebug : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        float radius = GetComponent<CircleCollider2D>().radius;

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(this.transform.position, radius*2);
    }
}
