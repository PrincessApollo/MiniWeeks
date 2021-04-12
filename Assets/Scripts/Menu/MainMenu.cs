using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Anton
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);       //Starts the game with the start button
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("quit");      //Quits the game when you hit the button that this method is attatched to
    }
}
