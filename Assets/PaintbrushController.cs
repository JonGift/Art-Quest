using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintbrushController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Test() {
        //Debug.Log("Hi1111");
        transform.rotation = transform.parent.rotation;
    }
}
