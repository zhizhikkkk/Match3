using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource[] destroyNoise;
    private bool isSoundOff= false;

    public void PlayRandomDestroyNoise()
    {
        int clipToPLay = UnityEngine.Random.Range(0, destroyNoise.Length);
        destroyNoise[clipToPLay].Play();
    }
    public void ToggleSound()
    {
        isSoundOff = !isSoundOff; // Переключаем состояние
        AudioListener.pause = isSoundOff; // Включаем или выключаем звук
    }
}
