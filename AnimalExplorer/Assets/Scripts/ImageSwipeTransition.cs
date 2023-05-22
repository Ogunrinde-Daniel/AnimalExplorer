using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class RectTransformExtensions
{
    //https://answers.unity.com/questions/888257/access-left-right-top-and-bottom-of-recttransform.html
    public static void SetLeft(this RectTransform rt, float left)
    {
        rt.offsetMin = new Vector2(left, rt.offsetMin.y);
    }

    public static void SetRight(this RectTransform rt, float right)
    {
        rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
    }

    public static void SetTop(this RectTransform rt, float top)
    {
        rt.offsetMax = new Vector2(rt.offsetMax.x, -top);
    }

    public static void SetBottom(this RectTransform rt, float bottom)
    {
        rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
    }
}


public class ImageSwipe : MonoBehaviour
{
    public Image image;
    public float transitionDuration = 1.0f; // Duration of the transition in seconds

    private void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine(ImageTransitionCoroutine());
    }

    private IEnumerator ImageTransitionCoroutine()
    {
        // Set up the rotation animation
        Quaternion startRotation = Quaternion.identity;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, -180f); // Rotate image 90 degrees counter-clockwise


        float timer = 0f;

        while (timer < transitionDuration)
        {
            timer += Time.deltaTime;

            image.rectTransform.rotation = Quaternion.Lerp(startRotation, targetRotation, timer / transitionDuration);
            yield return null;
        }

    }
}


public class ImageSwipeTransition : MonoBehaviour
{
    public Image[] images;
    private int currentIndex = -1;
    public float transitionTime = 1.0f;
    public int buildIndex;

    public AudioClip swipeAudio;
    private void Start()
    {
        gameObject.AddComponent<SfxPlayer>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) ||
                (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            gameObject.GetComponent<SfxPlayer>().playSfx(swipeAudio);
            currentIndex++;
            if (currentIndex >= images.Length)
                currentIndex = 0;

            StartImageTransition();
        }
    }

    private void StartImageTransition()
    {
        if (currentIndex >= images.Length - 1)
        {
            SceneManager.LoadScene(buildIndex);
            return;
        }
        images[currentIndex].AddComponent<ImageSwipe>();
        images[currentIndex].GetComponent<ImageSwipe>().transitionDuration = transitionTime;

    }

}

