using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityChecker : MonoBehaviour
{
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void CheckSection() {
        if (transform.position.z >= 0)
            rb.useGravity = false;
        else
            rb.useGravity = true;
    }
}
