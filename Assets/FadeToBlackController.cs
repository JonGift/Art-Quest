using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class FadeToBlackController : MonoBehaviour
{
    public GameObject square;
    Image squareImage;
    public float fadeSpeed = 1.5f;
    bool canFade = false;
    bool fadingIn = false;

    public GameObject XRRig;
    XRController leftHand;
    XRController rightHand;

    void Start() {
        //squareImage = Camera.main.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        squareImage = square.GetComponent<Image>();
        StartCoroutine(returnFromBlackAtStart());
        leftHand = XRRig.transform.GetChild(0).GetChild(1).GetComponent<XRController>();
        rightHand = XRRig.transform.GetChild(0).GetChild(2).GetComponent<XRController>();
    }

    public bool getCanFade() {
        return fadingIn;
    }

    public bool canOpenDoor() {
        return XRRig.GetComponent<ContinuousMovement>().GetEitherHandEmpty();
    }

    // Update is called once per frame
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

        while(squareImage.color.a < 1.2f) {
            fadeAmount = color.a + (fadeSpeed * Time.deltaTime);

            color = new Color(color.r, color.g, color.b, fadeAmount);
            squareImage.color = color;
            yield return null;
        }
        StartCoroutine(returnFromBlack());
    }

    public void SwapRooms(List<GameObject> toDisable, List<GameObject> toEnable) {
        foreach (GameObject g in toDisable)
            g.SetActive(false);

        foreach (GameObject g in toEnable)
            g.SetActive(true);
    }

    public IEnumerator returnFromBlack() {
        leftHand.enableInputActions = true;
        rightHand.enableInputActions = true;
        fadingIn = true;
        Color color = squareImage.color;
        float fadeAmount;

        while (squareImage.color.a > 0) {
            fadeAmount = color.a - (fadeSpeed * Time.deltaTime);

            color = new Color(color.r, color.g, color.b, fadeAmount);
            squareImage.color = color;
            yield return null;
        }

        canFade = true;
        fadingIn = false;
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
