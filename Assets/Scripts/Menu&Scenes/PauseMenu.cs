using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    bool active = false;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (active)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        active = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        active = false;
    }

    public void Home(int sceneID)
    {
        Time.timeScale = 1f;

        if (SceneManager.GetActiveScene().buildIndex == sceneID)
            Debug.LogWarning("Se ha intentado cargar la escena actual!");
        else
            SceneManager.LoadScene(sceneID);
    }
}
