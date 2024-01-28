using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System;

[System.Serializable]
struct dialogueLine
{
    public string text;
    public float startWaitTimeSeconds;
    //public float endWaitTimeSeconds;
    public float letterSpeedSeconds;
    public float fontSize;

    [Header("Events")]
    public UnityEvent startLineEvent;
    public UnityEvent endLineEvent;
}

[System.Serializable]
struct dialogueCharacter
{
    public string name;
    public List<dialogueLine> dialogueList;
}

public class DialogueSystem : MonoBehaviour
{


    public static event Action ImportantClientEvent = delegate { };
    public static event Action EndDialogueEvent = delegate { };
    public static event Action EndDespedidaEvent = delegate { };

    private void Awake()
    {
        if (!limoncin)
        {
            dialogueBox = transform.Find("TextBox").gameObject;
            dialogueTMP = dialogueBox.transform.Find("DialogueText").GetComponent<TextMeshProUGUI>();
            Cliente.ClientEnter += startImportance;
            Cliente.ClientExit += dialogueStop;
        Cliente.ClientSatisfiedEvent += startDespedida;
        }
        else
        {
            dialogueBox = transform.Find("LemonTextBox").gameObject;
            dialogueTMP = dialogueBox.transform.Find("LemonDialogueText").GetComponent<TextMeshProUGUI>();
            defaultFontColor = Color.yellow;
            Limoncin.LimoncinEvent += startCoroutines;
        }
            
    }

    void startImportance(bool importance, string clientname)
    {

        if (importance)
        {
            //do something
            //lanzar evento de cliente importante, lookeara la camara en un punto
            ImportantClientEvent();
        }
        //testing de que el cliente es importante cuando no
        startCoroutines(clientname);
    }

    void startDespedida(bool importance, string clientname)
    {
        if (importance)
        {
            //do something
            //lanzar evento de cliente importante, lookeara la camara en un punto
            ImportantClientEvent();
        }
        //testing de que el cliente es importante cuando no
        StartDespedidas(clientname);
    }

    public void startTutorial()
    {
        dialogueTMP.color = defaultFontColor;
        dialogueTMP.font = defaultFont;
        StopAllCoroutines();
        StartCoroutine(dialogueStart("Tutorial"));
    }
     void startCoroutines(string clientName)
    {
        dialogueTMP.color = defaultFontColor;
        dialogueTMP.font = defaultFont;
        StopAllCoroutines();
        //Debug.Log(limoncin);
        StartCoroutine(dialogueStart(clientName));
    }

    void StartDespedidas(string clientName)
    {
        dialogueTMP.color = defaultFontColor;
        dialogueTMP.font = defaultFont;
        StopAllCoroutines();
        //Debug.Log(limoncin);
        StartCoroutine(despedidasStart(clientName));
    }

    public IEnumerator despedidasStart(string clientName)
    {
        // Get characterEvent
        bool found = false;
        short i = 0;
        while (!found && i < charactersOut.Count)
        {
            if (charactersOut[i].name == clientName)
            {
                found = true;
                break;
            }

            i++;
        }
        if (((limoncin && clientName == "Limoncin") || !limoncin) && found)
        {
            dialogueBox.SetActive(true);
            yield return StartCoroutine(PrintDialogue(charactersOut[i].dialogueList, true, clientName));
        }

    }

    public IEnumerator dialogueStart(string clientName)
    {

        // Get characterEvent
        bool found = false;
        short i = 0;
        while (!found && i < characters.Count)
        {
            if (characters[i].name == clientName)
            {
                found = true;
                break;
            }

            i++;
        }
        if (((limoncin && clientName == "Limoncin") || !limoncin) && found)
        {
            dialogueBox.SetActive(true);
            yield return StartCoroutine(PrintDialogue(characters[i].dialogueList, false, clientName));
        }

    }
    private IEnumerator PrintDialogue(List<dialogueLine> dialogueList, bool despedida, string name)
    {

        short dialogueIndex = 0;
        while (dialogueIndex < dialogueList.Count)
        {
            if (dialogueList[dialogueIndex].startWaitTimeSeconds > 0)
            {
                dialogueBox.SetActive(false);
                yield return new WaitForSeconds(dialogueList[dialogueIndex].startWaitTimeSeconds);
                dialogueBox.SetActive(true);
            }

            dialogueList[dialogueIndex].startLineEvent?.Invoke();

            dialogueTMP.text = "";

            yield return StartCoroutine(letterByLetter(dialogueList[dialogueIndex], name));

            dialogueList[dialogueIndex].endLineEvent?.Invoke();

            //if (dialogueList[dialogueIndex].endWaitTimeSeconds > 0)
            //    yield return new WaitForSeconds(dialogueList[dialogueIndex].endWaitTimeSeconds);

            dialogueIndex++;
        }

        if (limoncin)
            yield return new WaitForSeconds(limoncin_time_till_dialogue_disappears);
        else
        {
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            EndDialogueEvent();
            if (despedida)
                EndDespedidaEvent();
        }

        dialogueStop();
    }
    
    private IEnumerator letterByLetter(dialogueLine dialogue, string name)
    {
        //TODO SONIDO DE HABLAR
        if (name == "Limoncin")
        {

            FMOD_Manager.instance.SetGlobalParameterByName("CharacterName", 2);
            FMOD_Manager.instance.PlaySingleInstanceEmitterControllerGroup("CustomerTalk");
        }
        else if (name == "Limoniano")
        {

            FMOD_Manager.instance.SetGlobalParameterByName("CharacterName", 1);
            FMOD_Manager.instance.PlaySingleInstanceEmitterControllerGroup("CustomerTalk");
        }
        else
        {

            FMOD_Manager.instance.SetGlobalParameterByName("CharacterName", 0);
            FMOD_Manager.instance.PlaySingleInstanceEmitterControllerGroup("CustomerTalk");
        }

        char[] messageArray = dialogue.text.ToCharArray();
        //Speed
        if (dialogue.letterSpeedSeconds <= 0)
            dialogue.letterSpeedSeconds = defaultLetterSpeed;
        //Size
        if (dialogue.fontSize <= 0)
            dialogue.fontSize = defaultFontSize;

        dialogueTMP.fontSize = dialogue.fontSize;

        for (int i = 0; i < messageArray.Length; i++)
        {
            dialogueTMP.text += messageArray[i];

            yield return new WaitForSeconds(dialogue.letterSpeedSeconds);
        }

        if (limoncin)
            yield return new WaitForSeconds(limoncin_time_till_dialogue_disappears);
        else
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
    }
    private void dialogueStop()
    {
        // Maybe hide textbox
        StopAllCoroutines();
        //Debug.Log(limoncin);
        dialogueTMP.text = "";
        dialogueBox.SetActive(false);

    }

    public void changeFont(TMP_FontAsset font)
    {
        dialogueTMP.font = font;
    }
    public void changeToLemonianColor()
    {
        dialogueTMP.color = Color.yellow;
    }

    [SerializeField]
    private List<dialogueCharacter> characters;
    [SerializeField]
    private List<dialogueCharacter> charactersOut;

    [SerializeField]
    private float defaultLetterSpeed = 0.04f;

    [SerializeField]
    private float defaultFontSize = 36f;

    [SerializeField]
    TMP_FontAsset defaultFont;

    Color defaultFontColor = Color.white;

    private GameObject dialogueBox;
    private TextMeshProUGUI dialogueTMP;

    [SerializeField]
    bool limoncin;
    const float limoncin_time_till_dialogue_disappears = 1f;
}