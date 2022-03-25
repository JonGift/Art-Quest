using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonController : MonoBehaviour
{
    private static SingletonController _instance;

    public static SingletonController Instance { get { return _instance; } }

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
