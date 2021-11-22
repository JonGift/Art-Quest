using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomHider : MonoBehaviour
{
    public List<GameObject> enablingObjects;
    public List<GameObject> disablingObjects;

    // Start is called before the first frame update
    void Start()
    {
        //enablingObjects = new List<GameObject>();
        //disablingObjects = new List<GameObject>();
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("WOWWW");
        if(other.gameObject.tag == "Player") {
            foreach (GameObject g in enablingObjects)
                g.SetActive(true);
            foreach (GameObject g in disablingObjects)
                g.SetActive(false);
        }
    }
}
