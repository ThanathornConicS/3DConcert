using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class BackRow_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public GameObject[] WideSpotlights;
        
    [SerializeField]
    public GameObject[] NarrowSpotlights;

    private Vector3 OriginalRot;

    public Animator animator;

    //Maxs Mins
    [SerializeField]
    private float WideIntensityMax, NarrowIntensityMax;
    void Start()
    {
        OriginalRot = new Vector3(0.0f, 0.0f, 0.0f);
        ResetAngles();

        //Row 1
        WideIntensityMax = 1.8f;
        //Row 2
        NarrowIntensityMax = 2.0f;

        animator = this.GetComponent<Animator>();

        //turn off all lights
        for (int i = 0; i < WideSpotlights.Length; i++)
            ChangeIntensityWide(i, 0.0f);

        for (int i = 0; i < NarrowSpotlights.Length; i++)
            ChangeIntensityNarrow(i, 0.0f);

    }

    [Button]
    private void ResetAngles()
    {
        for (int i = 0; i < WideSpotlights.Length; i++)
            WideSpotlights[i].transform.localEulerAngles = OriginalRot;

        for (int j = 0; j < NarrowSpotlights.Length; j++)
            NarrowSpotlights[j].transform.localEulerAngles = OriginalRot;
    }

    [Button]
    private void ResetIntensity()
    {
        for (int i = 0; i < WideSpotlights.Length; i++)
            ChangeIntensityWide(i, 1.0f);

        for (int j = 0; j < NarrowSpotlights.Length; j++)
            ChangeIntensityNarrow(j, 1.0f);
    }

    [Button]
    private void TurnoffBackrow()
    {
        for (int i = 0; i < WideSpotlights.Length; i++)
            ChangeIntensityWide(i, 0.0f);

        for (int j = 0; j < NarrowSpotlights.Length; j++)
            ChangeIntensityNarrow(j, 0.0f);
    }

    public void ChangeIntensityWide(int i, float intensity) //intensity b/w 0-1
    {
        float val = Mathf.Lerp(0.0f, WideIntensityMax, intensity);
        WideSpotlights[i].GetComponent<Light>().intensity = val;
        
    }

    public void ChangeIntensityNarrow(int i, float intensity) //intensity b/w 0-1
    {
        float val = Mathf.Lerp(0.0f, NarrowIntensityMax, intensity);
        NarrowSpotlights[i].GetComponent<Light>().intensity = val;
    }

    public void ChangeGradientNarrow(int i, Gradient g)
    {
        NarrowSpotlights[i].GetComponent<VLB.VolumetricLightBeam>().colorGradient = g;
        //change light color to the first value of gradient
        NarrowSpotlights[i].GetComponent<Light>().color = g.Evaluate(0.0f);
    }

    public void ChangeGradientWide(int i, Gradient g)
    {
        WideSpotlights[i].GetComponent<VLB.VolumetricLightBeam>().colorGradient = g;
        //change light color to the first value of gradient
        WideSpotlights[i].GetComponent<Light>().color = g.Evaluate(0.0f);
    }

    public void changeAnimSpeed(float sp)
    {
        animator.speed = sp;
    }

    public void PlayUnderwater()
    {
        EverythingFalse();
        animator.SetBool("underwater", true);
    }

    public void PlaySpiral()
    {
        EverythingFalse();
        animator.SetBool("spiral", true);
    }

    public void PlayMiddleWave()
    {
        EverythingFalse();
        animator.SetBool("wavesM", true);
    }

    public void AllBright()
    {
        EverythingFalse();
        animator.SetBool("allBright", true);
    }
    public void AllDim()
    {
        EverythingFalse();
        animator.SetBool("allDim", true);
    }

    public void FrontBack()
    {
        EverythingFalse();
        animator.SetBool("FB", true);
    }

    void EverythingFalse()
    {
        //set every animation parameter to false
        animator.SetBool("FB", false);
        animator.SetBool("wideIn", false);
        animator.SetBool("narrowIn", false);
        animator.SetBool("underwater", false);
        animator.SetBool("wavesM", false);
        animator.SetBool("allDim", false);
        animator.SetBool("allBright", false);
        animator.SetBool("spiral", false);
    }
}