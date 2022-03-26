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
        if (Directory.Exists(Application.persistentDataPath + "/paintings")) {
            string paintingsFolder = Application.persistentDataPath + "/paintings";

            DirectoryInfo d = new DirectoryInfo(paintingsFolder);
        } else {
            Directory.CreateDirectory(Application.persistentDataPath + "/paintings");
            return;
        }

        if(saveRandom || loadRandom) {
            if (Directory.Exists(Application.persistentDataPath + "/paintings/random")) {
                string paintingsFolder = Application.persistentDataPath + "/paintings/random";

                DirectoryInfo d = new DirectoryInfo(paintingsFolder);
            } else {
                Directory.CreateDirectory(Application.persistentDataPath + "/paintings/random");
                return;
            }
        }


        if (loadSpecific && specificName != "") {
            byte[] byteArray = File.ReadAllBytes(Application.persistentDataPath + "/paintings/" + specificName);
            Texture2D texture = new Texture2D(256, 256);
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

    void Start() {
        Invoke("LateStart", 3f);
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
        if(saveSpecific && specificName != "") {
            //string path = EditorUtility.SaveFilePanel("Save to png", Application.dataPath, textureName + "_painted.png", "png");
            string path = Application.persistentDataPath + "/paintings/" + specificName;
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
                    AssetDatabase.Refresh();
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
