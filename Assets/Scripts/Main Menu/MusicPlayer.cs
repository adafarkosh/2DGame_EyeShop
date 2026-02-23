using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    private static MusicPlayer instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // prevent duplicates when loading next scenes
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // keep playing across scenes
    }
}


