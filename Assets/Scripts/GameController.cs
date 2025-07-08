using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    int progressAmount;
    public Slider progressSlider;

    public GameObject player;
    public GameObject LoadCanvas;
    public List<GameObject> levels;
    private int currentLevelIndex;

    public GameObject gameOverScreen;
    public TMP_Text survivedText;
    private int survivedLevelsCount;

    public static event Action OnReset;

    void Start()
    {
        progressAmount = 0;
        progressSlider.value = 0;
        Gem.OnGemCollect += IncreaseProgressAmount;
        HoldToLoadLevel.OnHoldComplete += LoadNextLevel;
        PlayerHealth.OnPlayerDied += GameOverScreen;
        LoadCanvas.SetActive(false);
        gameOverScreen.SetActive(false);
    }

    void GameOverScreen()
    {
        gameOverScreen.SetActive(true);
        MusicManager.PauseBackgroundMusic();
        survivedText.text = "You survived " + survivedLevelsCount + " LEVEL!";

        if(survivedLevelsCount != 1) survivedText.text += "s";
        Time.timeScale = 0;
    }

    public void ResetGame()
    {
        gameOverScreen.SetActive(false);
        MusicManager.PlayBackgroundMusic(false);
        survivedLevelsCount = 0;
        LoadLevel(0, false);
        OnReset?.Invoke();
        Time.timeScale = 1;
    }
    
    
    void IncreaseProgressAmount(int amount)
    {
        progressAmount += amount;
        progressSlider.value = progressAmount;
        if (progressAmount >= 100)
        {
            LoadCanvas.SetActive(true);
            Debug.Log("Level Complete");
        }
    }

    void LoadLevel(int level, bool wantSurvivedIncrease)
    {
        LoadCanvas.SetActive(false);

        levels[currentLevelIndex].gameObject.SetActive(false);
        levels[level].gameObject.SetActive(true);

        player.transform.position = new Vector3(0, 0, 0);

        currentLevelIndex = level;
        progressAmount = 0;
        progressSlider.value = 0;
        if(wantSurvivedIncrease)survivedLevelsCount++;

    }

    

    void Update()
    {
        Debug.Log("Level Complete");

    }
    void LoadNextLevel()
    {
        int nextLevelIndex = (currentLevelIndex == levels.Count - 1) ? 0 : currentLevelIndex + 1;
        LoadLevel(nextLevelIndex, true);
    }
}
