using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ContinuousMovement : MonoBehaviour
{
    public XRNode inputSource;
    private Vector2 inputAxis;
    private CharacterController character;
    public LayerMask groundLayer;
    private XRRig rig;
    public GameObject headset;

    public float additionalHeight = .2f;

    public float speed = 1;
    public float gravity = -9.81f;
    private float fallingSpeed;

    bool secondaryButtonLeft;
    bool primaryButtonLeft;
    float buttonTimer = 0f;

    public bool rightHandEmpty = true;
    public bool leftHandEmpty = true;

    public bool cheatcodes = false;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        rig = GetComponent<XRRig>();
        //transform.rotation = Quaternion.Euler(0, -headset.transform.eulerAngles.y + transform.eulerAngles.y, 0);
        Invoke("Reorient", 1f);
        Invoke("Reposition", 2f);
        if (Application.isEditor){
            cheatcodes = true;
            GetComponent<DeviceBasedSnapTurnProvider>().enableTurnLeftRight = true;
        }
    InvokeRepeating("CheatCheck", 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
        device.TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryButtonLeft);
        device.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButtonLeft);
    }

    private void FixedUpdate() {
        CapsuleFollowHeadset();

        if (cheatcodes) {
            Quaternion headYaw = Quaternion.Euler(0, rig.cameraGameObject.transform.eulerAngles.y, 0);
            Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);


            character.Move(direction * Time.fixedDeltaTime * speed);

            if (secondaryButtonLeft)
                Reposition();
        }


        bool isGrounded = CheckIfGrounded();
        if (isGrounded)
            fallingSpeed = 0;
        else
            fallingSpeed += gravity * Time.fixedDeltaTime;
        
        character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
    }

    void CheatCheck() {
        if (cheatcodes)
            return;

        if (primaryButtonLeft) {
            buttonTimer += 1;
            if (buttonTimer > 5f) {
                cheatcodes = true;
                GetComponent<DeviceBasedSnapTurnProvider>().enableTurnLeftRight = true;
            }
                
        } else
            buttonTimer = 0;
    }

    bool CheckIfGrounded() {
        /*Vector3 rayStart = transform.TransformPoint(character.center);
        float rayLength = character.center.y + .005f;

        return Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);*/
        if(transform.position.y > 0) {
            return false;
        }
        return true;
    }

    void CapsuleFollowHeadset() {
        character.height = rig.cameraInRigSpaceHeight + additionalHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.cameraGameObject.transform.position);
        character.center = new Vector3(capsuleCenter.x, character.height / 2 + character.skinWidth, capsuleCenter.z);
    }

    public void Reposition() {
        //transform.position = new Vector3(transform.position.x - headset.transform.position.x, transform.position.y, transform.position.z - headset.transform.position.z);
        transform.position = new Vector3(transform.position.x - headset.transform.position.x, transform.position.y, transform.position.z - headset.transform.position.z);
        
    }

    public void Reorient() {
        transform.rotation = Quaternion.Euler(0, -headset.transform.eulerAngles.y + transform.eulerAngles.y, 0);
    }

    public void SetRightHandEmpty(bool tf) {
        rightHandEmpty = tf;
    }

    public void SetLeftHandEmpty(bool tf) {
        leftHandEmpty = tf;
    }

    public bool GetEitherHandEmpty() {
        return leftHandEmpty || rightHandEmpty;
    }
}
