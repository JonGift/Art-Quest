using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HallwayController : MonoBehaviour
{
    private static HallwayController _instance;

    public static HallwayController Instance { get { return _instance; } }

    bool enableChild;

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }

        DontDestroyOnLoad(this.gameObject);

        enableChild = false;
    }
    void Start() {
        SceneManager.activeSceneChanged += ToggleSelf;
    }

    private void OnDestroy() {
        SceneManager.activeSceneChanged -= ToggleSelf;
    }
    public void ToggleSelf(Scene c, Scene p) {
        transform.GetChild(0).gameObject.SetActive(enableChild);
        enableChild = !enableChild;
    }
}
