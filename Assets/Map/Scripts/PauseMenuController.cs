using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public GameObject winMenu;
    public static bool isGameWon = false;
    public static bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        winMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerHealth.gameOver) {
            EndGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused) {
                ResumeGame();
            } else {
                PauseGame();
            }
        }

        if (isGameWon) {
            WinGame();
        }
        
    }

    public void PauseGame() {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame() {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void GoToMenu() {
        Destroy(GameObject.Find("Player"));
        Destroy(GameObject.Find("Canvas"));
        Destroy(GameObject.Find("Clone"));
        Destroy(GameObject.Find("MenuCanvas"));
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);

    }

    public void EndGame() {
        Time.timeScale = 0f;
        isPaused = true;
        gameOverMenu.SetActive(true);
        PlayerHealth.gameOver = false;
    }

    public void RestartGame() {
        Destroy(GameObject.Find("Player"));
        Destroy(GameObject.Find("Canvas"));
        Destroy(GameObject.Find("Clone"));
        Destroy(GameObject.Find("MenuCanvas"));
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void WinGame() {
        Time.timeScale = 0f;
        isPaused = true;
        winMenu.SetActive(true);
        PlayerHealth.gameOver = false;
    }
}
