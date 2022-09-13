using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBlender : MonoBehaviour
{
    public Material sky;
    private float blendVal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sky = RenderSettings.skybox;
        SetBlend(blendVal);
        blendVal += 0.005f;
    }

    private void SetBlend(float val) {
        sky.SetFloat("_Blend", val);
    }
}
