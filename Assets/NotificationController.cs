using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationController : MonoBehaviour
{
    public GameObject target;
    Rigidbody rb;
    Material mat;

    // Start is called before the first frame update
    void Start()
    {
        mat = transform.GetChild(0).GetComponent<Renderer>().material;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target) {
            transform.LookAt(target.transform, Vector3.up);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
            //transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position);
        }
    }

    public void UpdateTarget(GameObject t) => target = t;

    private void OnCollisionEnter(Collision collision) {
        //Destroy(gameObject);
        rb.AddForce(new Vector3(Random.Range(-30f, 30f), Random.Range(-30f, 30f), Random.Range(-30f, 30f)));
        StartCoroutine(fadeToBlack());
    }

    public IEnumerator fadeToBlack() {
        Color color = mat.color;
        float fadeAmount;
        //camera.enabled = false;
        while (mat.color.a > 0f) {
            fadeAmount = color.a - (.25f * Time.deltaTime);

            color = new Color(color.r, color.g, color.b, fadeAmount);
            mat.color = color;
            yield return null;
        }

        Destroy(gameObject);
        //StartCoroutine(returnFromBlack());
    }
}
