using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwapController : MonoBehaviour
{
    public List<GameObject> attachedObjects; // Enables all objects here
    public List<GameObject> interruptingObjects; // Disables all objects here
    public List<GameObject> randomObjects; // Enables one random object
    FadeToBlackController fadeCheck;

    private void Start() {
        fadeCheck = FindObjectOfType<FadeToBlackController>();
    }

    public void swapAfterDelay() {
        if(!fadeCheck.getCanFade())
            StartCoroutine(swapAfterDelayEnum());
    }

    public IEnumerator swapAfterDelayEnum() {
        yield return new WaitForSeconds(1f);
        DisableInterruptingObjects();
        EnableAttachedObjects();
    }

    public void EnableAttachedObjects() {
        foreach (GameObject g in attachedObjects)
            g.SetActive(true);

        if (randomObjects != null) {
            if (randomObjects.Count > 0) {
                int i = Random.Range(0, randomObjects.Count);
                if(randomObjects[i] != null)
                    randomObjects[i].SetActive(true);
            }
        }
        
    }

    public void DisableInterruptingObjects() {
        foreach (GameObject g in interruptingObjects)
            g.SetActive(false);
    }
}
