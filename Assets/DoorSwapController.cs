using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwapController : MonoBehaviour
{
    public List<GameObject> attachedObjects;
    public List<GameObject> interruptingObjects;

    public void swapAfterDelay() {
        StartCoroutine(swapAfterDelayEnum());
    }

    public IEnumerator swapAfterDelayEnum() {
        yield return new WaitForSeconds(1.5f);
        DisableInterruptingObjects();
        EnableAttachedObjects();
    }

    public void EnableAttachedObjects() {
        foreach (GameObject g in attachedObjects)
            g.SetActive(true);
    }

    public void DisableInterruptingObjects() {
        foreach (GameObject g in interruptingObjects)
            g.SetActive(false);
    }
}
