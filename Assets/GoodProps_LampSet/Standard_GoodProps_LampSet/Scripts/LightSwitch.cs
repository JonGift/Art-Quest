using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
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

            //material emission
            tempMaterial.SetColor("_EmissionColor", new Color(0.5f, 0.5f, 0.5f));

        }
        else
        {
            lightObject.SetActive(false);

            //material emission
            tempMaterial.SetColor("_EmissionColor", Color.black);

        }
        _rend.sharedMaterial = tempMaterial;
        _wasLightOn = isLightOn;
    }
}
