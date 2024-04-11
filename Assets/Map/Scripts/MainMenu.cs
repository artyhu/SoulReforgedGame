using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame() {
        SceneManager.LoadScene(1);
    }

    public void HowToPlay() {
        SceneManager.LoadScene(2);
    }

    public void MenuScreen() {
        SceneManager.LoadScene(0);
    }

}
