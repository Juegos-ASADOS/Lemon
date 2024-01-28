using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static event Action<int> EndOfDay = delegate { };

    public static GameManager Instance { get; private set; }

    private const int StartingDayScene = 1;
    private const int EndingDayScene = 11;

    private int actualSceneNumber = StartingDayScene;

    float timer = 0;
    bool end = false;
    int day = 1;

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
    private void Update()
    {
        if (end)
        {
            if (timer < 0)
            {
                SceneManager.LoadScene("Day" + day);
                end = false;
            }
            timer -= Time.deltaTime;
        }
    }

    public void finishDay()
    {
        EndOfDay(0);
    }

    public void endDay()
    {
        day++;
        timer = 3;
        end = true;
    }
}
