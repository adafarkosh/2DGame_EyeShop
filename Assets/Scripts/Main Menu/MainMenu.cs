using UnityEngine; // tutorials: https://medium.com/@Brian_David/scene-loading-in-unity-a-comprehensive-guide-for-creating-main-menus-ui-elements-842d8ed3d364 && https://www.youtube.com/watch?v=DX7HyN7oJjE
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
