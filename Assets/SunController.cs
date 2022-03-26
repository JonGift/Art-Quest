using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = .25f;
    public bool yAxis = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!yAxis) {
            transform.Rotate(Vector3.right * Time.deltaTime * speed);
        } else {
            transform.Rotate(Vector3.up * Time.deltaTime * speed);
        }

    }
}
