using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource[] destroyNoise;

    public void PlayRandomDestroyNoise()
    {
        int clipToPLay = UnityEngine.Random.Range(0, destroyNoise.Length);
        destroyNoise[clipToPLay].Play();
    }

}
