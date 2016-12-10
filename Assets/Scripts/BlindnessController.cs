using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class BlindnessController : MonoBehaviour {

    VignetteAndChromaticAberration vignette;
    public float percentage = 1f;

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

	// Update is called once per frame
	void Update () {
		
	}
}
