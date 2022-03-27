using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitchHDRP : MonoBehaviour
{
    public GameObject lightObject;
    private Renderer _rend;

    public bool isLightOn = true;
    private bool _wasLightOn = false;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        _rend = GetComponent<Renderer>();
        SwitchLight();
    }

    // Update is called once per frame
    void Update()
    {
        //check the state of light
        if (isLightOn != _wasLightOn)
        {
            SwitchLight();
        }
    }

    void SwitchLight()
    {
        var tempMaterial = new Material(_rend.sharedMaterial);

        //turn on or off the light
        //and change the emission of the lamp
        if (isLightOn)
        {
            lightObject.SetActive(true);

            //HDRP Lit material emission
            tempMaterial.SetFloat("_EmissiveExposureWeight", 0.2f);

        }
        else
        {
            lightObject.SetActive(false);

            //HDRP Lit material emission
            tempMaterial.SetFloat("_EmissiveExposureWeight", 1f);


        }
        _rend.sharedMaterial = tempMaterial;
        _wasLightOn = isLightOn;
    }
}
