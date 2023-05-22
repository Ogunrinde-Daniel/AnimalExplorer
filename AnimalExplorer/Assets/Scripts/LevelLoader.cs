using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;

[Serializable]
public class KeyValuePair
{
    public string animalName;
    public Sprite animalSprite;
    public AudioClip animalSound;
}

public class LevelLoader : MonoBehaviour
{
    
    [SerializeField] private List<KeyValuePair> animalList = new List<KeyValuePair>();
    private Dictionary<string, Sprite> animalImageDict =  new Dictionary<string, Sprite>();
    private Dictionary<string, AudioClip> animalSoundDict =  new Dictionary<string, AudioClip>();

    [SerializeField] private List <Button> guiImages = new List<Button>();
    [SerializeField] private List <Button> guiNames = new List<Button>();

    void Awake()
    {
        //fill the dictionary with the list-
        //I used the list initially because you can't fill dictionaries in the unity editor
        foreach (var animal in animalList)
        {
            animalImageDict[animal.animalName] = animal.animalSprite;
            animalSoundDict[animal.animalName] = animal.animalSound;
        }
        enableNames(false);
    }

    void Start()
    {
        startGame();
    }

    private void startGame()
    {
        enableNames(false);
        HighlightImage(null, 0.2f);
        generateLevel();
    }


    public void HighlightImage(Button Image, float alpha = 0.5f)
    {
        Color color = Color.white;
        for (int i = 0; i < guiImages.Count; i++)
        {
            color = guiImages[i].image.color;
            if (guiImages[i].Equals(Image)) color.a = alpha;
            else color.a = 1.0f;
            guiImages[i].image.color = color;
        }

    }

    public void enableNames(bool value)
    {
        Color color = Color.white;
        for (int i = 0; i < guiNames.Count; i++)
        {
            enableName(value, guiNames[i]);

        }
    }

    public void enableName(bool value, Button name)
    {
        Color color = Color.white;

        name.interactable = value;
        color = name.GetComponentInChildren<TextMeshProUGUI>().color;
        if (value)
            color.a = 1.0f;
        else
            color.a = 0.2f;
        name.GetComponentInChildren<TextMeshProUGUI>().color = color;
    }


    public void drawLine(Button name, Button image, GameObject linePrefab)
    {
        GameObject lineObj = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        LineRenderer lineRenderer = lineObj.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;

        Vector3[] positions = new Vector3[2];
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, name.transform.position);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        positions[0] = worldPos;
        screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, image.transform.position);
        worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        positions[1] = worldPos;

        lineRenderer.SetPositions(positions);
    }

    public bool isCorrect(string name, Sprite image)
    {
        return (animalImageDict[name].Equals(image));
    }

    public AudioClip getAnimalSound(string name)
    {
        return animalSoundDict[name];
    }

    void generateLevel()
    {
        Dictionary<string, Sprite> currentDict = generateRandomDict(guiImages.Count);
        //insert all images in normal order
        for (int i = 0; i < guiImages.Count; i++)
        {
            guiImages[i].image.sprite = currentDict.ElementAt(i).Value;
        }

        //randomize the order of the names
        int index = 0;
        for (int i = 0; i < guiNames.Count; i++)
        {
            index = UnityEngine.Random.Range(0, currentDict.Count);
            guiNames[i].GetComponentInChildren<TextMeshProUGUI>().text = currentDict.ElementAt(index).Key;
            currentDict.Remove(currentDict.ElementAt(index).Key);
        }

    }

    Dictionary<string, Sprite> generateRandomDict(int size)
    {
        Dictionary<string, Sprite> randomDict = new Dictionary<string, Sprite>();
        int index = 0;
        for (int i = 0; i < size; i++)
        {
            index = UnityEngine.Random.Range(0, animalImageDict.Count);
            if (randomDict.ContainsKey(animalImageDict.ElementAt(index).Key))
            {
                i--;
                continue;
            }
            randomDict[animalImageDict.ElementAt(index).Key] = animalImageDict.ElementAt(index).Value;

        }

        return randomDict;
    }


}
