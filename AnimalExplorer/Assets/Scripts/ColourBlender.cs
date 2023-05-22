using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColourBlender : MonoBehaviour
{
    public SpriteRenderer image;
    public List<Color> colors = new List<Color>();
    
    public float blendTime = 2f; // Time in seconds to blend the colors
    
    private float timer = 0f;
    private bool isBlending = false;
    private int currentIndex = 0;

    public void Start()
    {
        StartColorBlend();
    }

    private void Update()
    {
        if (isBlending)
        {
            timer += Time.deltaTime;
            float blendProgress = Mathf.Clamp01(timer / blendTime);
            Color startColor = colors[currentIndex];
            Color endColor = colors[(currentIndex + 1) % colors.Count];
            Color currentColor = Color.Lerp(startColor, endColor, blendProgress);
            image.color = currentColor;

            if (blendProgress >= 1f)
            {
                timer = 0f;
                currentIndex = (currentIndex + 1) % colors.Count;
            }
        }
    }

    public void StartColorBlend()
    {
        if (colors.Count < 2)
        {
            Debug.LogWarning("Color list should contain at least 2 colors for blending.");
            return;
        }

        timer = 0f;
        currentIndex = 0;
        isBlending = true;
    }
}