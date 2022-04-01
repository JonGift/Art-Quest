using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PongPaddleController : MonoBehaviour
{
    public PongRobotController robot;
    public Text text;
    int score = 0;
    Rigidbody rb;
    string scoreText = "Score";
    AudioSource source;

    GameObject lastCollision;

    float keepAlive = 5f;

    Vector3 startPos;

    float difficulty = 1f;

    float previousZVel;
    // Start is called before the first frame update
    void Awake()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.right;
        source = GetComponent<AudioSource>();
    }

    private void Update() {
        keepAlive -= Time.deltaTime;
        if (keepAlive < 0f)
            resetPos();
        //if (rb.velocity.z == 0)
          //  rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, previousZVel);
        //previousZVel = rb.velocity.z;
    }

    private void resetPos() {
        transform.position = startPos;
        rb.velocity = Vector3.zero;
        lastCollision = null;
        Invoke("resetPosDelay", 1f);
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
        source.Play();
        //rb.velocity = -rb.velocity;
        if (coll.collider.CompareTag("Pong")) {
            if (coll.gameObject == lastCollision)
                return;
            lastCollision = coll.gameObject;
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
            
        }
    }

    private void OnTriggerEnter(Collider coll) {
        source.Play();
        Vector3 vel;
        vel.z = -rb.velocity.z;
        //vel.z += Random.Range(-.1f, .1f);

        if (rb.velocity.x < 0)
            vel.x = -1 * difficulty;
        else
            vel.x = 1 * difficulty;
        vel.y = 0;

        rb.velocity = vel;
        if (coll.gameObject.CompareTag("WallPlayer")) {
            //difficulty -= .0001f;
            if (difficulty < 1)
                difficulty = 1;
            robot.changeDifficulty(false);
            resetPos();
        } else if (coll.gameObject.CompareTag("WallRobot")) {
            difficulty += .03f;
            ++score;
            if (score > 50)
                scoreText = "OKAY YOU DID IT GOOD JOB PLEASE STOP :(";
            else if (score > 30)
                scoreText = "PROFESSIONAL PONGER";
            else if (score > 20)
                scoreText = "Mondo Score Dude!";
            else if (score > 10)
                scoreText = "Wow!";
            else if (score > 5)
                scoreText = "Good job!";
            else
                scoreText = "Score";

            text.text = scoreText + "\n" + score;
            robot.changeDifficulty(true);
            resetPos();
        }
    }
}
