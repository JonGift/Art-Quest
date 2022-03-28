using UnityEngine;
using System.Collections;

using UnityEditor;

[CustomEditor(typeof(GifViewer))]
public class GifViewerEditor : Editor
{

	
	private SerializedProperty gifPathOrURL;

	private SerializedProperty gifBytes;

	private Texture choosenGif;

	private Rect gifPreviewArea;

	public void OnEnable()
	{
		gifPathOrURL = serializedObject.FindProperty("gifPathOrURL");
		gifBytes = serializedObject.FindProperty("gifBytes");
		reloadGuiPreview();
	}

	private void reloadGuiPreview()
	{
		choosenGif = (Texture)AssetDatabase.LoadAssetAtPath("Assets/" + gifPathOrURL.stringValue, typeof(Texture));
	}

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		EditorGUILayout.Space();
		DropAreaGUI();
	}

	public void DropAreaGUI()
	{
		Event evt = Event.current;
		float dragNDropBoxHeight = 20.0f;
		if (choosenGif == null) dragNDropBoxHeight = 100.0f;
		Rect drop_area = GUILayoutUtility.GetRect(0.0f, dragNDropBoxHeight, GUILayout.ExpandWidth(true));
		GUI.Box(drop_area, "Drag'n'Drop your GIF");
		float wantedGifPreviewWidth = Screen.width- Screen.width/3;
		gifPreviewArea = GUILayoutUtility.GetRect(wantedGifPreviewWidth, calculateGifPreviewHeight(choosenGif, wantedGifPreviewWidth), GUILayout.ExpandWidth(true));

		switch (evt.type)
		{
			case EventType.DragUpdated:
			case EventType.DragPerform:
				if (!drop_area.Contains(evt.mousePosition) && !gifPreviewArea.Contains(evt.mousePosition))
					return;

				DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

				if (evt.type == EventType.DragPerform)
				{
					DragAndDrop.AcceptDrag();

					foreach (Object dragged_object in DragAndDrop.objectReferences)
					{
						string assetPath = AssetDatabase.GetAssetPath(dragged_object);
						if (assetPath.StartsWith("Assets/")){
							assetPath = assetPath.Substring(7);
						}
						if (assetPath.ToLower().EndsWith(".gif")){
							//Debug.Log("dragged_object = " + assetPath);
							gifPathOrURL.stringValue = assetPath;
							reloadGuiPreview();
							GUI.DrawTexture(gifPreviewArea, choosenGif);
							string bytesFilePath = assetPath.Replace(".gif", ".bytes").Replace(".GIF", ".bytes");
							Debug.Log("Copying " + assetPath + " to " + bytesFilePath);
							//FileUtil.ReplaceFile("Assets/"+assetPath, "Assets/"+bytesFilePath);
							AssetDatabase.CopyAsset("Assets/" + assetPath, "Assets/" + bytesFilePath);
							gifBytes.objectReferenceValue = AssetDatabase.LoadAssetAtPath("Assets/" + bytesFilePath, typeof(TextAsset));
							
						}

						
					}
				}
				break;
		}
	
		GUI.DrawTexture(gifPreviewArea, choosenGif,ScaleMode.ScaleAndCrop);

		// Apply values to the target
		serializedObject.ApplyModifiedProperties();
	}

	public float calculateGifPreviewHeight(Texture texture,float wantedWidth)
	{
		if (texture == null) return 0;
		float width = texture.width;
		float height = texture.height;
		return (wantedWidth / width) * height;


	}
}