using UnityEngine;
using System.Collections;

/// <summary>
/// React to a hit from a Projectile object by creating a GameObject (probably a particle effect).
/// </summary>
public class HitByProjectile : MonoBehaviour {

	public GameObject collisionParticleEffect;

	public int hitsBeforeDestruction = 3;

	public void Hit(Vector3 position, Projectile byObject) {
		if(collisionParticleEffect != null) {
			Instantiate (collisionParticleEffect, position, transform.rotation);
		}
		if(hitsBeforeDestruction > 0)
		{
			hitsBeforeDestruction --;
			if(hitsBeforeDestruction == 0)
				Destroy(gameObject);
		}
	}

	void OnCollisionStay(Collision collision) {
		Hit (collision.contacts [0].point, collision.gameObject.GetComponent<Projectile>());
	}
}
