using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalTrigger : MonoBehaviour
{
    public List<GameObject> attachedObjects;
    public List<GameObject> interruptingObjects;
    public VisionTrigger visionTriggerOptional;

    public void EnableAttachedObjects() {
        foreach (GameObject g in attachedObjects)
            g.SetActive(true);
    }

    public void DisableInterruptingObjects() {
        foreach (GameObject g in interruptingObjects)
            g.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            if(visionTriggerOptional != null) {
                visionTriggerOptional.EnableAttachedObjects();
                visionTriggerOptional.DisableInterruptingObjects();
            } else {
                EnableAttachedObjects();
                DisableInterruptingObjects();
            }
        }
    }

    public void CallTrigger() {
        if (visionTriggerOptional != null) {
            visionTriggerOptional.EnableAttachedObjects();
            visionTriggerOptional.DisableInterruptingObjects();
        } else {
            EnableAttachedObjects();
            DisableInterruptingObjects();
        }
    }
}
