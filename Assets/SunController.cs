using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = .25f;
    public bool yAxis = false;
    public float additionalModifier = 0f; // positive only
    public float maxSpeed = 0f; // positive only

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!yAxis) {
            transform.Rotate(Vector3.right * Time.deltaTime * speed);
        } else {
            transform.Rotate(Vector3.up * Time.deltaTime * speed);
        }

        if(additionalModifier != 0f && speed < maxSpeed)
            speed += additionalModifier;
    }
}
