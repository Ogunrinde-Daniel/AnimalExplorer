using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //bool matchCompleted = false;
    private Button selectedImage;
    private Button selectedName;

    public ProgressSlider progress;
    public LevelLoader levelLoader;
    public AudioSource audioSource;
    public AudioClip animalMismatchedSfx;

    public GameObject redLinePrefab;
    public GameObject greenLinePrefab;


    public GameObject gameOverScreen;
    public GameObject gameOverParticles;

    public ParticleSystem winParticle;
    public ParticleSystem loseParticle;
    public ParticleSystem clickParticle;


    private int totalAnimals = 5;
    private int animalsMatched = 0;
    private int correctMatches = 0;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(clickParticle, mousePosition, Quaternion.identity);
        }
    }

    void gameOver()
    {
        gameOverParticles.SetActive(true);

        gameOverScreen.SetActive(true);
        gameOverScreen.GetComponentInParent<Canvas>().sortingOrder++;
        if (correctMatches > 3)
        {
            gameOverScreen.GetComponentInChildren<TextMeshProUGUI>().text = "Great Job\n" + correctMatches + "/" + totalAnimals;
        }
        else
        {
            gameOverScreen.GetComponentInChildren<TextMeshProUGUI>().text = "Let's Try Again\n" + correctMatches + "/"+  totalAnimals;
        }

    }


    void CalculateResult()
    {
        if (levelLoader.isCorrect(selectedName.GetComponentInChildren<TextMeshProUGUI>().text, selectedImage.image.sprite))
        {
            correctMatches++;
            audioSource.clip = levelLoader.getAnimalSound(selectedName.GetComponentInChildren<TextMeshProUGUI>().text);

            displayParticleOnCenter(winParticle);
            levelLoader.drawLine(selectedName, selectedImage, greenLinePrefab);
        }
        else
        {
            audioSource.clip = animalMismatchedSfx;

            displayParticleOnCenter(loseParticle);
            levelLoader.drawLine(selectedName, selectedImage, redLinePrefab);

        }

        audioSource.Play();

        animalsMatched++;
        progress.updateSlider(animalsMatched, totalAnimals);
        levelLoader.enableName(false, selectedName);
        
        if(animalsMatched >= totalAnimals) gameOver();
    }

    public void displayParticleOnCenter(ParticleSystem particle)
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        Vector3 worldCenter = Camera.main.ScreenToWorldPoint(screenCenter);
        Instantiate(particle, (Vector2)worldCenter, Quaternion.identity);
        
    }


    public void ImageClick(Button image)
    {
        selectedImage = image;
        levelLoader.HighlightImage(image, 0.2f);
        if(animalsMatched == 0)levelLoader.enableNames(true);
    }

    public void NameClick(Button name)
    {
        selectedName = name;
        CalculateResult();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }


}
