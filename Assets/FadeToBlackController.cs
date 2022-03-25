using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class FadeToBlackController : MonoBehaviour
{
    public GameObject square;
    Image squareImage;
    public float fadeSpeed = 1.5f;
    bool canFade = false;
    bool fadingIn = false;

    bool opIsDone = false;

    public GameObject XRRig;
    XRController leftHand;
    XRController rightHand;

    Camera camera;

    void Start() {
        //squareImage = Camera.main.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        squareImage = square.GetComponent<Image>();
        StartCoroutine(returnFromBlackAtStart());
        leftHand = XRRig.transform.GetChild(0).GetChild(1).GetComponent<XRController>();
        rightHand = XRRig.transform.GetChild(0).GetChild(2).GetComponent<XRController>();
        camera = GetComponent<Camera>();
    }

    public bool getCanFade() {
        return fadingIn;
    }

    public bool canOpenDoor() {
        return XRRig.GetComponent<ContinuousMovement>().GetEitherHandEmpty();
    }

    public void callFade() {
        if (!canOpenDoor()) return;

        leftHand.enableInputActions = false;
        rightHand.enableInputActions = false;

        canFade = false;
        StartCoroutine(fadeToBlack());
    }

    public IEnumerator fadeToBlack() {
        Color color = squareImage.color;
        float fadeAmount;
        //camera.enabled = false;
        while (squareImage.color.a < 1.2f && !opIsDone) {
            fadeAmount = color.a + (fadeSpeed * Time.deltaTime);

            color = new Color(color.r, color.g, color.b, fadeAmount);
            squareImage.color = color;
            yield return null;
        }
        //StartCoroutine(returnFromBlack());
    }

    public void SwapRooms(List<GameObject> toDisable, List<GameObject> toEnable) {
        foreach (GameObject g in toDisable)
            g.SetActive(false);

        foreach (GameObject g in toEnable)
            g.SetActive(true);
    }

    public void StartReturnFromBlack() {
        
    }

    public void PassSceneAsync(string s) {
        AsyncOperation op = SceneManager.LoadSceneAsync(s);
        StopCoroutine(fadeToBlack());
        StartCoroutine(returnFromBlack(op));
    }

    public IEnumerator returnFromBlack(AsyncOperation op) {

        while (!opIsDone) {
            Debug.Log("woww");
            if (op.isDone) {
                opIsDone = true;
            } else {
                yield return null;
            }
        }

        Debug.Log("aaa");
        fadingIn = true;
        Color color = squareImage.color;
        float fadeAmount = 0f;


        while (squareImage.color.a > 0) {
            Debug.Log(squareImage.color.a);
            fadeAmount = color.a - (fadeSpeed * Time.deltaTime);

            color = new Color(color.r, color.g, color.b, fadeAmount);
            squareImage.color = color;
            yield return null;
        }
        leftHand.enableInputActions = true;
        rightHand.enableInputActions = true;
        canFade = true;
        fadingIn = false;
        opIsDone = false;
        Debug.Log("Done");
        //camera.enabled = true;

    }

    public IEnumerator returnFromBlackAtStart() {
        yield return new WaitForSeconds(1);
        XRRig.GetComponent<ContinuousMovement>().Reposition();
        Color color = squareImage.color;
        float fadeAmount;

        while (squareImage.color.a > 0) {
            fadeAmount = color.a - (fadeSpeed * Time.deltaTime);

            color = new Color(color.r, color.g, color.b, fadeAmount);
            squareImage.color = color;
            yield return null;
        }

        canFade = true;
    }
}
