using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTestScript : MonoBehaviour
{
    [SerializeField] ParticleSystem muzzleFlash;
    //[SerializeField] ParticleSystem 

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            muzzleFlash.Play();
        } else if (Input.GetMouseButtonUp(0))
        {
            muzzleFlash.Stop();
        }
    }
}
