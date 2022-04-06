using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HockeySpawner : MonoBehaviour
{
    public GameObject puck;
    public GameObject parentThing;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("MakePong", 30f, 30f);
    }

    void MakePong() {
        Instantiate(puck, Vector3.zero, Quaternion.identity, parentThing.transform);
    }
}
