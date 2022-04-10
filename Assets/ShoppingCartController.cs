using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingCartController : MonoBehaviour
{
    public GameObject box;
    public float yMod = 6;

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Grab")) {
            if (!other.GetComponent<Rigidbody>()) {
                GameObject temp = other.transform.parent.gameObject;
                temp.GetComponent<Rigidbody>().velocity = Vector3.zero;
                temp.transform.position = new Vector3(box.transform.position.x, box.transform.position.y + yMod, box.transform.position.z);
            } else {
                other.GetComponent<Rigidbody>().velocity = Vector3.zero;
                other.transform.position = new Vector3(box.transform.position.x, box.transform.position.y + yMod, box.transform.position.z);
            }
        }

    }
}
