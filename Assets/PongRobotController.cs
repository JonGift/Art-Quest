using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongRobotController : MonoBehaviour
{
    public GameObject ball;
    public float difficulty = .01f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, ball.transform.position.z + Random.Range(-.003f, .003f)), difficulty * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision) {
        difficulty += .01f;
    }

    public void changeDifficulty(bool tf) {
        if (tf)
            difficulty += .01f;
        else
            difficulty -= .01f;
    }
}
