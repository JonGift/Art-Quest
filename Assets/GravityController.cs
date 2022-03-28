using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    public bool enableGravity = false;

    private void OnTriggerEnter(Collider other) {
        if(other.tag != "Player") {
            other.GetComponent<Rigidbody>().useGravity = enableGravity;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag != "Player") {
            other.GetComponent<Rigidbody>().useGravity = !enableGravity;
        }
    }
}
