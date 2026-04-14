using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadGame()
    {
        /// load the game scene
        SceneManager.LoadScene(1); //* File -> Build Profiles -> Start scene (0), Game scene (1)
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }

}
