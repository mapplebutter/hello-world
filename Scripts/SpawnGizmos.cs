using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGizmos : MonoBehaviour {

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position, new Vector3(0.1f, 0.1f, 0.1f));
    }
}
