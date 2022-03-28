using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GifViewer : UniGifImage {

	// Start to play gif at game start
	public bool playGifOnStart;

	// Start to play gif when player enter Collider
	public bool playGifOnColliderEntered;

	// The Collider that trig the gif play
	public Collider ColliderTrigger;

	// Path of the GIF file, relative to Assets folder
	public string gifPathOrURL;

	public TextAsset gifBytes;

	// Full path of GIF
	private string gifFullPath;

	// Collider is already entered.
	private bool entered = false;

	

	// Use this for initialization
	protected new void Start () {
		base.Start();
		if (gifPathOrURL.StartsWith("http") || gifPathOrURL.StartsWith("https"))
		{
			gifFullPath = gifPathOrURL;
		}
		else
		{
			gifFullPath = Path.Combine(Application.dataPath, gifPathOrURL);
			gifFullPath = Path.Combine("file:///", gifFullPath);
		}

		StartCoroutine(ViewGifCoroutine());
	
		


	}
	private IEnumerator ViewGifCoroutine()
	{

		byte[] bytes = null;
		if (gifBytes != null) bytes = gifBytes.bytes;

		yield return StartCoroutine(SetGifFromUrlCoroutine(gifFullPath, playGifOnStart, bytes));
	}

	// Update is called once per frame
	protected new void Update () {
		base.Update();
	}

	public void playGif()
	{
		
		Play();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (playGifOnColliderEntered && !entered)
		{
			playGif();
			entered = true;
		}
	}
}
