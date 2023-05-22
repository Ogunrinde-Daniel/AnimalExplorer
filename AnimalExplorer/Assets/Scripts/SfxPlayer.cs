using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxPlayer : MonoBehaviour
{
    private SoundManager soundManager;
    void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }

    public void playSfx(AudioClip clip)
    {
        soundManager.playSfx(clip);
    }
}
