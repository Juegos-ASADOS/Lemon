using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Image fadeImage;

    [SerializeField]
    private float delay;

    [SerializeField]
    private Color Alpha0;

    [SerializeField]
    private Color Alpha1;

    public bool sceneFading;

    public static event Action<int> EndOfDay = delegate { };

    public static GameManager Instance { get; private set; }

    private const int StartingDayScene = 1;
    private const int EndingDayScene = 11;

    private int actualSceneNumber = StartingDayScene;

    float timer = 0;
    bool end = false;
    public int day = 1;

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
                if(day < 8)
                    SceneManager.LoadScene("Day" + day);
                else
                    SceneManager.LoadScene("MainMenu");
                end = false;
               StopAllCoroutines();
               StartCoroutine(FadeIn());
            }
            timer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
            end = true;

        if (Input.GetKeyDown(KeyCode.Return))
            endDay();
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
        sceneFading = true;
        StartCoroutine(FadeOut());
    }


    private IEnumerator FadeOut()
    {
        while ((fadeImage.color.a < 1) || (sceneFading == true))
        {
            fadeImage.color = Color.Lerp(fadeImage.color, Alpha1, delay * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(delay);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        yield return null;
    }

    private IEnumerator FadeIn()
    {
        while ((fadeImage.color.a > 0) || (sceneFading == true))
        {
            fadeImage.color = Color.Lerp(fadeImage.color, Alpha0, delay * Time.deltaTime);
            yield return null;
        }
        sceneFading = false;
        yield return null;
    }
}
