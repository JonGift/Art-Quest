using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class VisionTriggerCameraController : MonoBehaviour
{
    Camera cam;

    List<GameObject> visionTriggers;
    public GameObject hallways;
    GameObject hallwayChild;

    float viewAngle = 50f;

    void Start() {
        visionTriggers = new List<GameObject>();
        SceneManager.activeSceneChanged += UpdateTriggers;
        InvokeRepeating("findTriggers", 0, .125f);
    }

    private void OnDestroy() {
        SceneManager.activeSceneChanged -= UpdateTriggers;
    }

    void OnLevelWasLoaded() {



    }

    void findTriggers() {
        if(hallways != null)
            if (!hallwayChild.active)
                return;
        
        RaycastHit hit;
        foreach (GameObject g in visionTriggers) {
            if (g) {
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

    public void UpdateTriggers(Scene c, Scene n) {
        hallways = FindObjectOfType<HallwayController>().gameObject;
        if (hallways)
            hallwayChild = hallways.transform.GetChild(0).gameObject;
        cam = GetComponent<Camera>();

        visionTriggers = new List<GameObject>();
        visionTriggers.Clear();
        foreach (VisionTrigger g in GameObject.FindObjectsOfType<VisionTrigger>(true))
            visionTriggers.Add(g.gameObject);

        visionTriggers = visionTriggers.Where(g => g != null).ToList();

        viewAngle = cam.fieldOfView;
        //InvokeRepeating("findTriggers", 0, .125f);
        Invoke("Clean", 2f);
    }

    void Clean() {
        visionTriggers = visionTriggers.Where(g => g != null).ToList();
    }
}
