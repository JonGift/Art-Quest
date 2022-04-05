using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxController : MonoBehaviour
{
    public Material skybox1;
    public Material skybox2;

    public void ChangeSkybox(int num) {
        if (num == 1)
            RenderSettings.skybox = skybox1;
        else
            RenderSettings.skybox = skybox2;
    }
}
