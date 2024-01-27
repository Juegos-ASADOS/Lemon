using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame(int sceneID)
    {
        if (SceneManager.GetActiveScene().buildIndex == sceneID)
            Debug.LogWarning("Se ha intentado cargar la escena actual!");
        else
            SceneManager.LoadScene(sceneID);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
