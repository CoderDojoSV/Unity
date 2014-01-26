using UnityEngine;
using System.Collections;

/// <summary>
/// Destroy an object after the specified amount of time.
/// </summary>
public class TimedDestroy : MonoBehaviour {

	public float duration = 3;

	// Use this for initialization
	void Start () {
		Destroy(gameObject, duration);
	}
}
