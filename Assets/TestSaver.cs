using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Es.InkPainter;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;

public class TestSaver : MonoBehaviour
{
    public Texture2D texRef;
    Material matSelf;
    InkCanvas inkCanvas;

    public bool save = false;

    // Start is called before the first frame update


    private void Awake() {
        matSelf = GetComponent<Renderer>().materials[0];
        if (texRef)
            matSelf.SetTexture("_BaseMap", texRef);
        inkCanvas = GetComponent<InkCanvas>();
    }

    void Start() {
        Invoke("LateStart", 5f);
        //if(save)
        //    InvokeRepeating("SaveRenderTextureToPNGAuto", 10f, 10f);
    }

    void LateStart() {

    }

    private void Update() {
        //RenderTexture rt = new RenderTexture(texRef.width / 2, texRef.height / 2, 0);
        //RenderTexture.active = rt;
        // Copy your texture ref to the render texture
        //Graphics.Blit(texRef, rt);
        //matSelf.SetTexture("_BaseMap", texRef);
    }

    public void SaveRenderTextureToPNGAuto() {
        if (!save)
            return;
        //string path = EditorUtility.SaveFilePanel("Save to png", Application.dataPath, textureName + "_painted.png", "png");
        string path = Application.dataPath + "/wowwwasdf.png";
        if (path.Length != 0) {
            RenderTexture renderTexture = inkCanvas.GetPaintMainTexture(matSelf.name);
            //RenderTexture renderTexture = GetComponent<Renderer>().materials[0].mainTexture;
            var newTex = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB4444, false, false);
            RenderTexture.active = renderTexture;
            newTex.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            newTex.Apply();

            byte[] pngData = newTex.EncodeToPNG();
            if (pngData != null) {
                File.WriteAllBytes(path, pngData);
                AssetDatabase.Refresh();
            }

            Debug.Log(path);
        }
    }

}
