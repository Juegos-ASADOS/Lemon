using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class DialogueSystem : MonoBehaviour
{
    private void Awake()
    {
        dialogueBox = transform.Find("TextBox").gameObject;
        dialogueTMP = dialogueBox.transform.Find("DialogueText").GetComponent<TextMeshProUGUI>();
        Cliente.ClientEnter += startCoroutines;
        Cliente.ClientExit += dialogueStop;
        //TODO: Evento de diálogo de limoncín (esto igual es con otro script)
    }

    void startCoroutines(string clientName)
    {
        StartCoroutine(dialogueStart(clientName));
    }

    public IEnumerator dialogueStart(string clientName)
    {

        dialogueBox.SetActive(true);

        // Get characterEvent
        bool found = false;
        short i = 0;
        while (!found && i < characters.Count)
        {
            if (characters[i].name == clientName)
                break;

            i++;
        }
        yield return StartCoroutine(PrintDialogue(characters[i].dialogueList));
    }
    private IEnumerator PrintDialogue(List<dialogueLine> dialogueList)
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

            yield return StartCoroutine(letterByLetter(dialogueList[dialogueIndex]));

            dialogueList[dialogueIndex].endLineEvent?.Invoke();

            //if (dialogueList[dialogueIndex].endWaitTimeSeconds > 0)
            //    yield return new WaitForSeconds(dialogueList[dialogueIndex].endWaitTimeSeconds);

            dialogueIndex++;
        }

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        dialogueStop();
    }
    private IEnumerator letterByLetter(dialogueLine dialogue)
    {
        //TODO SONIDO DE HABLAR
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

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
    }
    private void dialogueStop()
    {
        // Maybe hide textbox
        StopAllCoroutines();
        dialogueTMP.text = "";
        dialogueBox.SetActive(false);
    }
    //Can be called in the event part of the dialogue for any NPC
    public void changeTextFont()
    {
        
    }

    [SerializeField]
    private List<dialogueCharacter> characters;

    [SerializeField]
    private float defaultLetterSpeed = 0.04f;
    
    [SerializeField]
    private float defaultFontSize = 36f;

    private GameObject dialogueBox;
    private TextMeshProUGUI dialogueTMP;

    private GameObject limDialogueBox;
    private TextMeshProUGUI limDialogueTMP;

    //short dialogueIndex;
}

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