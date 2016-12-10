using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class BlindnessController : MonoBehaviour {

    VignetteAndChromaticAberration vignette;
    public float percentage = 1f;
    public float healPercentage = 0.25f;

    // Use this for initialization
    void Start () {
        vignette = GetComponent<VignetteAndChromaticAberration>();
        StartCoroutine(blinding());
	}
	
    IEnumerator blinding()
    {
        float intensity = 0f;

        while (intensity <= percentage)
        {
            yield return new WaitForSeconds(1/60);
            intensity += 0.005f;
            if (intensity > 1f)
            {
                intensity = 1f;
            }
            vignette.intensity = intensity;
        }
    }

    public void DoHeal()
    {
        StartCoroutine(heal());
    }

    IEnumerator heal()
    {
        float desiredIntensity = vignette.intensity - healPercentage;
        while(vignette.intensity >= desiredIntensity)
        {

            yield return new WaitForSeconds(1 / 60);
            float intensity = vignette.intensity - 0.005f;
            if (intensity < 0)
            {
                intensity = 0;
            }
            vignette.intensity = intensity;
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
