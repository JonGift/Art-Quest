using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;


public class PhysicalTriggerFinder : MonoBehaviour
{
    public LayerMask triggerLayer;

    private CharacterController character;



    // Start is called before the first frame update
    void Start() {
        character = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        checkTriggers();
    }

    void checkTriggers() {
        Vector3 rayStart = transform.TransformPoint(character.center);
        float rayLength = character.center.y + .01f;

        Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, triggerLayer);
        if (hitInfo.collider)
            hitInfo.collider.GetComponent<PhysicalTrigger>().CallTrigger();
    }
}
