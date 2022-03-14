using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public List<AudioSource> sources;
    public List<AudioClip> clips;

    public int clipIndex;

    // Start is called before the first frame update
    void Start()
    {
        clipIndex = Random.Range(0, clips.Count);
        playSong();
    }

    // Update is called once per frame
    void Update()
    {
        
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
