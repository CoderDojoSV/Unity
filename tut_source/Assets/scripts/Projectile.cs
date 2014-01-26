using UnityEngine;
using System.Collections;

/// <summary>
/// Script for projectile weapons.
/// Does a simple raycast to account for bullet-through-paper problems 
/// when interacting with HitByProjectile objects
/// </summary>
[RequireComponent (typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public float speed = 30f;
    public bool destroyOnHit = true;
    public bool disconnectAndStopParticlesOnDestroy = true;

    void Start()
    {
        rigidbody.velocity = transform.forward * speed;
    }

    void FixedUpdate()
    {
        float speed = rigidbody.velocity.magnitude;
        float distanceBetweenUpdates = speed * Time.deltaTime;
        if (distanceBetweenUpdates > 0)
        {
            Vector3 direction = rigidbody.velocity / speed;
            RaycastHit hitInfo;
            bool willCollide = Physics.Raycast(transform.position, direction,
                                               out hitInfo, distanceBetweenUpdates);
            if (willCollide && hitInfo.distance < distanceBetweenUpdates)
            {
                DoHit(hitInfo);
            }
        }
    }

    void DoHit(RaycastHit rh)
    {
        GameObject go = rh.collider.gameObject;
        HitByProjectile[] behaviors = go.GetComponents<HitByProjectile>();
        for (int i = 0; i < behaviors.Length; ++i)
        {
            behaviors [i].Hit(rh.point, this);
        }

        if (destroyOnHit)
        {
            Destroy(gameObject);
        }
    }

	void OnDestroy()
	{
		if (disconnectAndStopParticlesOnDestroy)
		{
			for(int i = 0; i < transform.childCount; ++i)
			{
				GameObject child = transform.GetChild(i).gameObject;
				child.transform.parent = transform.parent;
				ParticleSystem ps = child.GetComponent<ParticleSystem>();
				if(ps != null)
				{
					ps.enableEmission = false;
				}
				ParticleCulling pc = child.GetComponent<ParticleCulling>();
				if(pc != null)
				{
					pc.enabled = true;
				}
			}
		}
	}
}