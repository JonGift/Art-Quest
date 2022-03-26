using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactSoundController : MonoBehaviour
{
    AudioSource source;
    public float modifier = 1f;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.relativeVelocity.magnitude > 1) {
            source.Stop();
            source.volume = (.01f + (collision.relativeVelocity.magnitude / 12f)) * modifier;
            source.pitch = Random.Range(.9f, 1.1f);
            source.Play();
        }
    }
}
