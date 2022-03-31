using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongPaddleController : MonoBehaviour
{
    public GameObject target;
    public bool player = true;
    public PongRobotController robot;

    Rigidbody rb;

    float keepAlive = 5f;

    Vector3 startPos;

    float difficulty = 1f;

    // Start is called before the first frame update
    void Awake()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.right;
    }

    private void Update() {
        keepAlive -= Time.deltaTime;
        if (keepAlive < 0f)
            resetPos();
    }

    private void resetPos() {
        transform.position = startPos;
        rb.velocity = Vector3.zero;
        Invoke("resetPosDelay", 2f);
        keepAlive = 5f;

    }

    void resetPosDelay() {


        keepAlive = 5f;
        float rand = Random.Range(0, 2);
        if (rand < 1) {
            rb.velocity = -transform.right * difficulty;
        } else {
            rb.velocity = transform.right * difficulty;
        }
    }

    private void OnCollisionEnter(Collision coll) {
        //rb.velocity = -rb.velocity;
        if (coll.collider.CompareTag("Pong")) {
            keepAlive = 5f;
            Vector3 vel;
            if (rb.velocity.x < 0)
                vel.x = 1 * difficulty;
            else
                vel.x = -1 * difficulty;
            //vel.x = rb.velocity.x * -1;
            vel.y = 0;
            vel.z = rb.velocity.z + coll.collider.attachedRigidbody.velocity.z + Random.Range(-.1f, .1f);
                
            rb.velocity = vel;
        } else {
            keepAlive = 5f;
            Vector3 vel;
            if (rb.velocity.x < 0)
                vel.x = -1 * difficulty;
            else
                vel.x = 1 * difficulty;
            vel.y = 0;
            vel.z = -rb.velocity.z + Random.Range(-.1f, .1f);
            rb.velocity = vel;
            if (coll.collider.CompareTag("WallPlayer")) {
                difficulty -= .01f;
                if (difficulty < 1)
                    difficulty = 1;
                robot.changeDifficulty(false);
                resetPos();
            }else if (coll.collider.CompareTag("WallRobot")) {
                difficulty += .01f;
                robot.changeDifficulty(true);
                resetPos();
            }
        }
    }
}
