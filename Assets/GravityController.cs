using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    public bool enableGravity = false;

    private void OnTriggerEnter(Collider other) {
        if(other.tag != "Player") {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb == null)
                rb = other.transform.parent.GetComponent<Rigidbody>();

            if(rb)
                rb.useGravity = enableGravity;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag != "Player") {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb == null)
                rb = other.transform.parent.GetComponent<Rigidbody>();

            if (rb)
                rb.useGravity = !enableGravity;
        }
    }
}
