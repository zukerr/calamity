using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelController : MonoBehaviour
{
    private static LevelController instance;
    public static LevelController Instance
    {
        get
        {
            return instance;
        }
    }

    [SerializeField]
    private GameObject gameOverScreen = null;
    [SerializeField]
    private GameObject gameCompleteScreen = null;
    [SerializeField]
    private TextMeshProUGUI killCounterText = null;

    private int enemiesInLevel;
    private bool isGameFinished = false;
    private int killCounter = 0;

    private void Awake()
    {
        instance = this;
    }

    public void RegisterEnemy()
    {
        enemiesInLevel++;
    }

    public void EnemyEliminated()
    {
        enemiesInLevel--;
        killCounter++;
        killCounterText.text = $"Enemies killed: {killCounter}";
        if(IsGameComplete())
        {
            //TO-DO
            gameCompleteScreen.SetActive(true);
            isGameFinished = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
        }
    }

    public bool IsGameComplete()
    {
        return enemiesInLevel == 0;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(0);
    }

    public void OnGameOver()
    {
        gameOverScreen.SetActive(true);
        isGameFinished = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    public bool IsGameFinished()
    {
        return isGameFinished;
    }
}
