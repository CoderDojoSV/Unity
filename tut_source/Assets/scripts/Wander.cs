using UnityEngine;
using System.Collections;

/// <summary>
/// Wander steering behavior for a Character Motor
/// </summary>
public class Wander : MonoBehaviour {
	public float maxRotation = 90, turnTimer = 3;
	float rotation, timer;

	void FixedUpdate () {
		timer -= Time.deltaTime;
		if(timer <= 0)
		{
			rotation = Random.Range(-maxRotation, maxRotation);
			timer += Random.Range(0, turnTimer);
		}
		transform.rotation *= Quaternion.Euler (0, rotation * Time.deltaTime, 0);
		CharacterMotor cm = GetComponent<CharacterMotor> ();
		cm.inputMoveDirection = transform.forward;
	}
}
