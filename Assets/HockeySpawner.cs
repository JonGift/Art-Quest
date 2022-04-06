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
        InvokeRepeating("MakePong", 60f, 60f);
    }

    void MakePong() {
        Instantiate(puck, new Vector3(-0.3132f, 0.71204f, 0.3225f), parentThing.transform.rotation, parentThing.transform);
    }
}
