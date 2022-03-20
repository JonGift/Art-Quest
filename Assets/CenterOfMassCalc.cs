using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterOfMassCalc : MonoBehaviour
{
    public Vector3 center;
    public float sizeOfCircle;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = center;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + transform.rotation * center, sizeOfCircle);
    }
}
