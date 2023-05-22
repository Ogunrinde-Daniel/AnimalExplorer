using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SoundManager : MonoBehaviour
{
    public bool soundOn = true;

    public AudioSource bgSource;
    public AudioSource sfxSource;

    public AudioClip screenTapClip;

    public List<AudioClip> bgMusicList = new List<AudioClip>();

    private int currentBg = 0;

    void Start()
    {
        if (soundOn)
        {
            currentBg = Random.Range(0, bgMusicList.Count);
            playBgMusic();
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) ||
                (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            playSfx(screenTapClip);
        }
    }

    public void toggleAllSound(bool state)
    {

        if (state)
        {
            if (bgSource.clip == null)
            {
                currentBg = Random.Range(0, bgMusicList.Count);
                playBgMusic();
            }
                        
            bgSource.UnPause();            
        }
        else
        {
            bgSource.Pause();
        }

    }

    public void playBgMusic()
    {
        if(!soundOn){return;}
        
        currentBg = (currentBg + 1) % bgMusicList.Count;
        float clipLength = bgMusicList[currentBg].length;
        bgSource.clip = bgMusicList[currentBg];
        bgSource.Play();
        Invoke("playBgMusic", clipLength);
    }

    public void playSfx(AudioClip clip)
    {
        sfxSource.clip = clip;
        sfxSource.Play();
    }

}
