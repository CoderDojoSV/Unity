using UnityEngine;
using System.Collections;

/// <summary>
/// If this GameObject falls below the given y-dimension threshold,
/// teleport back to where it was when Start happened.
/// </summary>
public class RestartAfterFall : MonoBehaviour {

	Vector3 startLoc;

	public float minValue = -20;
    public AudioClip noise;

	void Start () {
		startLoc = transform.position;
	}

	public void Respawn()
	{
		transform.position = startLoc;
		CharacterMotor cm = GetComponent<CharacterMotor> ();
		if (cm != null) {
			cm.movement.velocity = Vector3.zero;
		}
		else if (rigidbody != null) {
			rigidbody.velocity = Vector3.zero;
		}
        if (noise != null)
        {
            PlaySound.Play(noise, transform);
        }
	}

	void FixedUpdate () {
		if(transform.position.y < minValue)
		{
			Respawn ();
		}
	}
}
