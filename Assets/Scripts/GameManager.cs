using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private DayBase actualDay;

    private const int StartingDayScene = 1;
    private const int EndingDayScene = 11;

    private int actualSceneNumber = StartingDayScene;

    public uint money = 0;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private IEnumerator ProcessDay()
    {
        //Wait 1 frame
        yield return null;

        if (actualDay == null){
            Debug.LogError("Couldn't find a DAY");
            yield break;
        }

        actualDay.InitDay();

        while (!actualDay.HasEnded())
            yield return null;

        Debug.Log("Día Terminado");

        if (actualSceneNumber == EndingDayScene) { } // Do smth

        actualSceneNumber += 1;
        SceneManager.LoadScene(actualSceneNumber);
    }

    private void OnLevelWasLoaded(int level)
    {
        actualDay = FindObjectOfType<DayBase>();
        StartCoroutine(ProcessDay());
    }
}
