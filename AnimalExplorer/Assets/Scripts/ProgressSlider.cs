using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressSlider : MonoBehaviour
{
    public Slider slider;
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void updateSlider(int value, int maxValue)
    {
        slider.GetComponentInChildren<TextMeshProUGUI>().text = value + "/" + maxValue;
        slider.value = (float)value/(float)maxValue;
    }
}
