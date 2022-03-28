using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouseMove : MonoBehaviour
{

	public float horizontalSpeed = 2.0f;
	public float verticalSpeed = 2.0f;

	private float xRot = 0.0f;
	private float yRot = 0.0f;

	// Start is called before the first frame update
	void Start()
    {
		xRot = transform.eulerAngles.x;
		yRot = transform.eulerAngles.y;

	}

    // Update is called once per frame
    void Update()
    {
		yRot += horizontalSpeed * Input.GetAxis("Mouse X");
		xRot -= verticalSpeed * Input.GetAxis("Mouse Y");

		transform.eulerAngles = new Vector3(xRot, yRot, 0.0f);
	}
}
