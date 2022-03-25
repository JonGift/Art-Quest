using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomVideoPlayer : MonoBehaviour
{
    public GameObject objDisplay;
    public int numberOfFrames = 0;
    public float fps = 30;
    public string filePath = "ConvertedVideos/Georgia";
    
    private AudioSource audio;
    private Renderer renderer;
    private Texture2D[] frames;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        renderer = objDisplay.GetComponent<Renderer>();

        frames = new Texture2D[numberOfFrames];
        for(int i = 0; i < numberOfFrames; i++) {
            frames[i] = (Texture2D)Resources.Load(string.Format(filePath + "/frame{0:d4}", i + 1));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!audio.isPlaying)
            audio.Play();
        int currentFrame = (int)(Time.time * fps);
        if (currentFrame >= frames.Length)
            currentFrame = frames.Length - 1;

        renderer.material.mainTexture = frames[currentFrame];
    }
}
