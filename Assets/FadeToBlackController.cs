using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlackController : MonoBehaviour
{
    public GameObject square;
    Image squareImage;
    public float fadeSpeed = 1;
    bool canFade = true;

    void Start() {
        //squareImage = Camera.main.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        squareImage = square.GetComponent<Image>();
    }

    // Update is called once per frame
    public void callFade() {
        if (!canFade) return;

        canFade = false;
        StartCoroutine(fadeToBlack());
    }

    public IEnumerator fadeToBlack() {
        Color color = squareImage.color;
        float fadeAmount;

        while(squareImage.color.a < 1.5f) {
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
