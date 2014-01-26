using UnityEngine;
using System.Collections;

/// <summary>
/// For creating AudioSources in temporary objects at a given transform.
/// </summary>
public class PlaySound : MonoBehaviour
{
    public AudioClip sound;
    public string buttonPress;

    void Start()
    {
        if (buttonPress == null || buttonPress.Length == 0)
        {
            Play(sound, transform);
        }
    }

    void Update()
    {
        if (Input.GetButtonDown(buttonPress))
        {
            Play(sound, transform);
        }
    }

    public static AudioSource Play(AudioClip ac, Transform emitter)
    {
        if (ac == null)
        {
            print ("can't play sound!");
            return null;
        }
        GameObject go = new GameObject("sound: " + ac.name);
        AudioSource asrc = go.AddComponent<AudioSource>();
        asrc.clip = ac;
        asrc.Play();
        Destroy(go, ac.length);
        if (emitter != null)
        {
            go.transform.position = emitter.transform.position;
            go.transform.parent = emitter.transform.parent;
        }
        return asrc;
    }
}
