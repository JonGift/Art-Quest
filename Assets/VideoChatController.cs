using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoChatController : MonoBehaviour
{
    public List<VideoPlayer> videos;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("LateStart", 3f);
    }

    // Update is called once per frame
    void LateStart()
    {
        GetComponent<AudioSource>().Play();
        foreach (VideoPlayer v in videos)
            v.Play();
    }
}
