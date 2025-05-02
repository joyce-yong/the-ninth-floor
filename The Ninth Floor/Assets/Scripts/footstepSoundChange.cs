using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footstepSoundChange : MonoBehaviour
{
    public AudioSource footSource, footSource2;
    public AudioClip foot1, foot2, foot3, foot4;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            footSource.enabled = false;
            footSource2.enabled = false;
            footSource.clip = foot3;
            footSource2.clip = foot4;
            footSource.enabled = true;
            footSource2.enabled = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            footSource.enabled = false;
            footSource2.enabled = false;
            footSource.clip = foot1;
            footSource2.clip = foot2;
            footSource.enabled = true;
            footSource2.enabled = true;
        }
    }
}