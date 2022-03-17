using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public List<AudioSource> sources;
    public List<AudioClip> clips;

    public int clipIndex;

    public HingeJoint joint;
    bool grabbingJoint = false;
    float baseVolume = 0.01f;
    bool shouldPlay = true;

    // Start is called before the first frame update
    void Start()
    {
        clipIndex = Random.Range(0, clips.Count);
        playSong();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldPlay && !sources[0].isPlaying) {
            clipIndex = Random.Range(0, clips.Count);
            playSong();
        }
        if (grabbingJoint) {
            foreach(AudioSource source in sources) {
                source.volume = baseVolume * ((-joint.angle + 159) / 100);
            }
        }
    }

    public void setShouldPlay(bool tf) => shouldPlay = tf;

    public void setGrabbing(bool tf) {
        grabbingJoint = tf;
    }

    void playSong() {
        foreach (AudioSource s in sources) {
            s.Stop();
            s.clip = clips[clipIndex];
            s.Play();
        }
    }

    public void nextSong() {
        if(clipIndex >= clips.Count - 1) {
            clipIndex = 0;
        } else {
            clipIndex++;
        }

        playSong();
    }

    public void previousSong() {
        if (clipIndex <= 0) {
            clipIndex = clips.Count - 1;
        } else {
            clipIndex--;
        }

        playSong();
    }
}
