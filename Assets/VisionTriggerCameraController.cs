using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionTriggerCameraController : MonoBehaviour
{
    Camera cam;

    List<GameObject> visionTriggers;

    float viewAngle = 50f;

    void Start() {
        cam = GetComponent<Camera>();

        visionTriggers = new List<GameObject>();
        foreach (VisionTrigger g in GameObject.FindObjectsOfType<VisionTrigger>(true))
            visionTriggers.Add(g.gameObject);

        viewAngle = cam.fieldOfView;
        InvokeRepeating("findTriggers", 0, .125f);
    }

    void findTriggers() {
        RaycastHit hit;
        foreach (GameObject g in visionTriggers) {
            Vector3 direction = g.transform.position - transform.position;
            if (Vector3.Angle(direction, transform.forward) < viewAngle) {
                Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z), (g.transform.position - transform.position), out hit);
                Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z), (g.transform.position - transform.position));
                if (hit.transform) {
                    if (hit.transform.tag == "VisionTrigger") {
                        hit.collider.GetComponent<VisionTrigger>().EnableAttachedObjects();
                        hit.collider.GetComponent<VisionTrigger>().DisableInterruptingObjects();
                    }
                }
            }
        }
    }
}
