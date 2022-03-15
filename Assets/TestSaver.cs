using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSaver : MonoBehaviour
{
    public Texture2D texRef;
    Material matSelf;
    
    // Start is called before the first frame update


    void Start()
    {
        matSelf = GetComponent<Renderer>().materials[0];
        Debug.Log(matSelf.mainTexture);
        matSelf.SetTexture("_BaseMap", texRef);
    }

    private void Update() {
        //RenderTexture rt = new RenderTexture(texRef.width / 2, texRef.height / 2, 0);
        //RenderTexture.active = rt;
        // Copy your texture ref to the render texture
        //Graphics.Blit(texRef, rt);
        //matSelf.SetTexture("_BaseMap", texRef);
    }
}
