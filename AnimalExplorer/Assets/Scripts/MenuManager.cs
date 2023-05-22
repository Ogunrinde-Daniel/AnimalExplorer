using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Sprite soundOnIcon;
    public Sprite soundOffIcon;
    public Image iconHolder;
    public bool soundOnIconState = true;
    public SoundManager soundManager;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void setImage()
    {
        if (soundOnIconState)
        {
            iconHolder.sprite = soundOnIcon;
        }else
        {
            iconHolder.sprite = soundOffIcon;
        }

    }

    public void toggleSound()
    {
        soundOnIconState = !soundOnIconState;
        setImage();
        soundManager.toggleAllSound(soundOnIconState);
    }

    public void openReference()
    {
        Application.OpenURL("https://drive.google.com/file/d/124O__uvylwmjqOd5Mvpw5oO9a5-THqqT/view?usp=share_link");
    }


}
