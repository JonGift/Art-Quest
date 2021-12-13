using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionTrigger : MonoBehaviour
{
    public List<GameObject> attachedObjects;
    public List<GameObject> interruptingObjects;

    public void EnableAttachedObjects() {
        if (attachedObjects == null)
            return;
        foreach (GameObject g in attachedObjects)
            g.SetActive(true);
    }

    public void DisableInterruptingObjects() {
        if (interruptingObjects == null)
            return;
        foreach (GameObject g in interruptingObjects)
            g.SetActive(false);
    }
}
