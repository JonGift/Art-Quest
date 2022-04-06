using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundscapeController : MonoBehaviour
{
    public List<AudioSource> sounds;
    public float timeRate = 1f;
    public float volumeIncrease = .01f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ModifySoundscape", timeRate, timeRate);
    }

    void ModifySoundscape() {
        foreach (AudioSource sound in sounds)
            sound.volume += volumeIncrease;
    }
}
