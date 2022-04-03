using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Es.InkPainter;
using System.IO;
using UnityEditor;
using UnityEngine.UI;

public class TestSaver : MonoBehaviour
{
    public Texture2D texRef;
    Material matSelf;
    InkCanvas inkCanvas;

    public bool saveSpecific = false;
    public bool saveRandom = false;
    public bool loadSpecific = false;
    public bool loadRandom = false;
    public string specificName = "";
    public Image Img;


    // Start is called before the first frame update


    private void Awake() {
        matSelf = GetComponent<Renderer>().materials[0];
        if (!Directory.Exists(Application.persistentDataPath + "/paintings")) {
            Directory.CreateDirectory(Application.persistentDataPath + "/paintings");
        }

        if(saveRandom || loadRandom) {
            if (!Directory.Exists(Application.persistentDataPath + "/paintings/random")) {
                Directory.CreateDirectory(Application.persistentDataPath + "/paintings/random");
            }
        }


        if (loadSpecific && specificName != "") {
            byte[] byteArray;
            try {
                byteArray = File.ReadAllBytes(Application.persistentDataPath + "/paintings/" + specificName + ".png");
            } catch {
                Debug.Log("error: file does not exist.");
                inkCanvas = GetComponent<InkCanvas>();
                return;
            }

            Texture2D texture = new Texture2D(256, 256, TextureFormat.ARGB4444, false);
            texture.LoadImage(byteArray);
            RenderTexture rt = new RenderTexture(128, 128, 0);
            RenderTexture.active = rt;
            Graphics.Blit(texture, rt);
            matSelf.SetTexture("_BaseMap", texture);
        } else if (loadRandom) {
            string[] temp = Directory.GetFiles(Application.persistentDataPath + "/paintings/random");
            byte[] byteArray = File.ReadAllBytes(temp[Random.Range(0, temp.Length)]); ;
            Texture2D texture = new Texture2D(256, 256);
            texture.LoadImage(byteArray);
            RenderTexture rt = new RenderTexture(128, 128, 0);
            RenderTexture.active = rt;
            Graphics.Blit(texture, rt);
            matSelf.SetTexture("_BaseMap", texture);
        }
        inkCanvas = GetComponent<InkCanvas>();
    }

    public void SaveRenderTextureToPNGAuto() {
        if(saveSpecific && specificName != "") {
            Debug.Log("Saving file: " + specificName);
            //string path = EditorUtility.SaveFilePanel("Save to png", Application.dataPath, textureName + "_painted.png", "png");
            string path = Application.persistentDataPath + "/paintings/" + specificName + ".png";
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
                    //AssetDatabase.Refresh();
                }
            }
        }else if (saveRandom) {
            string name = System.DateTime.Now.ToString("MM-dd-yyyy-h-mm-ss-tt");
            string path = Application.persistentDataPath + "/paintings/random/" + name + ".png";
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
                    //AssetDatabase.Refresh();
                }
            }
        }
    }

    private void OnApplicationQuit() {
        //SaveRenderTextureToPNGAuto();
    }

    private void OnDestroy() {
        //SaveRenderTextureToPNGAuto();
    }

}
