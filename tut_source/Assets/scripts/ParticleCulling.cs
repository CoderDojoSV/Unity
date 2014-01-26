using UnityEngine;
using System.Collections;

/// <summary>
/// Used to delete particle effects from the game world after they are done.
/// </summary>
public class ParticleCulling : MonoBehaviour
{
    ParticleSystem ps;
    public bool activated = false;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        if (ps == null)
        {
            print(this + " has no ParticleSystem");
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (activated || !ps.enableEmission)
        {
            if(ps.particleCount == 0)
                Destroy(gameObject);
        } else if (ps.particleCount != 0)
        {
            activated = true;
        }
    }
}
