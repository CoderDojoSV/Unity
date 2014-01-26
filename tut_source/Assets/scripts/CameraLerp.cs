using UnityEngine;
using System.Collections;

/// <summary>
/// Does a Liner-intERPolation with the main camera to this Transform.
/// "Gives the camera back" after some specified time.
/// </summary>
public class CameraLerp : MonoBehaviour
{
    Vector3 startLoc, startLocalPosition;
    Quaternion startRot, startLocalRotation;
    float timer;
    public float lerpDuration = 10, revertTime = 20;
    Camera cam;
	public string skipButton = "Fire1";
    void Start()
    {
        cam = Camera.main;
        startLoc = cam.transform.position;
        startRot = cam.transform.rotation;
        startLocalPosition = cam.transform.localPosition;
        startLocalRotation = cam.transform.localRotation;
    }
    void Update()
    {
		if (Input.GetButtonDown (skipButton)) {
			timer = revertTime;
		}
		// move camera back to the player for update purposes
		cam.transform.localPosition = startLocalPosition;
		cam.transform.localRotation = startLocalRotation;
	}
	void LateUpdate()
    {
        // move camera to more cinematic location just before the scene is drawn
        timer += Time.deltaTime;
        if (timer < lerpDuration)
        {
            cam.transform.position = Vector3.Lerp(startLoc, transform.position, timer / lerpDuration);
            cam.transform.rotation = Quaternion.Lerp(startRot, transform.rotation, timer / lerpDuration);
        } else if (timer < revertTime)
        {
            cam.transform.position = transform.position;
            cam.transform.rotation = transform.rotation;
        } else
        {
            Destroy(gameObject);
        }
    }
}
