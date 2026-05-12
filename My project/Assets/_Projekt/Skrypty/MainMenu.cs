using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    
    public void QuitGame()
    {
        Debug.Log("Gra zostaje wy³¹czona!");
        Application.Quit();
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}