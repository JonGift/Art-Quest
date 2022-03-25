using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonInteractionController : MonoBehaviour
{
    private static SingletonInteractionController _instance;

    public static SingletonInteractionController Instance { get { return _instance; } }

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }
}