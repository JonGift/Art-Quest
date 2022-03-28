using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchOnTV : MonoBehaviour
{
	public GifViewer gifViewer;

	private bool entered = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
		if (!entered)
		{
			gifViewer.playGif();
			entered = true;
		}
	}
}
