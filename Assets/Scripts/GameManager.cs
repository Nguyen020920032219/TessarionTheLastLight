using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void StartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(PlayerPrefs.GetInt("SaveScene"));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(1);
    }
}