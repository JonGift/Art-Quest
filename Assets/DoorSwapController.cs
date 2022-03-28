using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorSwapController : MonoBehaviour
{
    public List<GameObject> attachedObjects; // Enables all objects here
    public List<GameObject> interruptingObjects; // Disables all objects here
    public List<GameObject> randomObjects; // Enables one random object
    FadeToBlackController fadeCheck;

    public string sceneToSwapName; // If swapping scenes

    AsyncOperation op;
    FadeToBlackController controller;

    public List<AudioClip> doorSounds;
    AudioSource source;



    void Start() {
        source = GetComponent<AudioSource>();
        SceneManager.activeSceneChanged += FindFadeCheck;
        fadeCheck = FindObjectOfType<FadeToBlackController>();

    }

    private void OnDestroy() {
        SceneManager.activeSceneChanged -= FindFadeCheck;
    }

    public void FindFadeAndCallFade() {
        controller = FindObjectOfType<FadeToBlackController>();
        controller.callFade();
        source.clip = doorSounds[0];
        source.Play();
        if (!fadeCheck.getCanFade() && fadeCheck.canOpenDoor())
            StartCoroutine(swapAfterDelayEnum());
    }

    public void swapAfterDelay() {

    }

    public IEnumerator swapAfterDelayEnum() {
        yield return new WaitForSeconds(1f);
        DisableInterruptingObjects();
        if (sceneToSwapName != null && sceneToSwapName != "") {
            EnableAttachedObjects();
            SwapScene();
        } else {
            EnableAttachedObjects();
        }
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

    public void SwapScene() {
       // HallwayController temp = GameObject.FindObjectOfType<HallwayController>();
       // if (temp)
        //    temp.ToggleSelf();

        controller.PassSceneAsync(sceneToSwapName);
    }

    public void DisableInterruptingObjects() {
        foreach (GameObject g in interruptingObjects)
            g.SetActive(false);
    }

    void FindFadeCheck(Scene c, Scene s) {
        fadeCheck = FindObjectOfType<FadeToBlackController>();

        if(c.name == sceneToSwapName) {
            source.clip = doorSounds[1];
            source.Play();
        }
    }

    public void DoorCloses() {
        source.clip = doorSounds[1];
        source.Play();
    }
}
