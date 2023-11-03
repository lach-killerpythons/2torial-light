using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGlow : MonoBehaviour
{
    // Start is called before the first frame update
    public Material myMaterial; 
    private float myWeight;
    private float lightChange = 0.1f;

    public bool FlashingLight = true;
    private int frameCount = 0;
    public int oscillationSpeed = 100;
    
    //change brightness to new value between 0-1
    public void ChangeBrightness(float newBrightness)
    {
        //avoid floating point error
        
        if(myMaterial != null && newBrightness <= 1 && newBrightness > 0){
            newBrightness = Mathf.Round(newBrightness*10)*0.1f;
            myMaterial.SetFloat("_Weight", newBrightness);
            myWeight = myMaterial.GetFloat("_Weight"); 
        }
    }

    private void OscillateBrightness()
    {
        if(myWeight == 1.0f)
        {
            lightChange = -0.1f;
        }
        if (myWeight <= 0.1f)
        {
            lightChange = 0.1f;
        }
        float newBrightness = myWeight + lightChange;
        ChangeBrightness(newBrightness);
    }
    
    void Start()
    {
        // 
        frameCount = oscillationSpeed;

        myMaterial = GetComponent<Renderer>().material;
        myWeight = myMaterial.GetFloat("_Weight");
        Debug.Log(myWeight);
        //ChangeBrightness(0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (frameCount < oscillationSpeed)
        {
            frameCount ++;
        }
        else
        {
            if(FlashingLight){
                OscillateBrightness();
            }
            Debug.Log(myWeight);
            frameCount = 0;
        }        
    }
}
