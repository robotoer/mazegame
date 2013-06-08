using UnityEngine;
using System.Collections;

public class MouseLook : MonoBehaviour {
  public float sensitivityX = 1.5F;
  public float sensitivityY = 1.5F;

  public float minimumX = -360F;
  public float maximumX = 360F;

  public float minimumY = -60F;
  public float maximumY = 60F;

  float rotationX = 0F;
  float rotationY = 0F;

  Quaternion originalRotation;

	// Use this for initialization
	void Start () {
    originalRotation = transform.localRotation;
	}
	
	// Update is called once per frame
	void Update () {
    // Read the mouse input axis
    rotationX += Input.GetAxis("Mouse X") * sensitivityX;
    rotationY += Input.GetAxis("Mouse Y") * sensitivityY;

    rotationX = ClampAngle (rotationX, minimumX, maximumX);
    rotationY = ClampAngle (rotationY, minimumY, maximumY);

    Quaternion xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up);
    Quaternion yQuaternion = Quaternion.AngleAxis (rotationY, -Vector3.right);

    Camera.main.transform.localRotation = originalRotation * xQuaternion * yQuaternion;
	}

  public static float ClampAngle (float angle, float min, float max) {
    if (angle < -360F) {
      angle += 360F;
    }
    if (angle > 360F) {
      angle -= 360F;
    }
    return Mathf.Clamp (angle, min, max);
  }
}
