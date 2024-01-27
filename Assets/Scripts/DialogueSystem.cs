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
            Debug.Log(i);
            if (characters[i].name == clientName)
                break;

            i++;
        }
        yield return StartCoroutine(PrintDialogue(characters[i].dialogueList));
    }

    private IEnumerator PrintDialogue(List<dialogueLine> dialogueList)
    {
        dialogueIndex = 0;
        while (dialogueIndex < dialogueList.Count)
        {
            dialogueList[dialogueIndex].startLineEvent?.Invoke();

            dialogueTMP.text = "";

            yield return StartCoroutine(letterByLetter(dialogueList[dialogueIndex]));

            dialogueList[dialogueIndex].endLineEvent?.Invoke();

            dialogueIndex++;
        }

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        dialogueStop();
    }

    private IEnumerator letterByLetter(dialogueLine dialogue)
    {
        char[] messageArray = dialogue.text.ToCharArray();
        for (int i = 0; i < messageArray.Length; i++)
        {
            dialogueTMP.text += messageArray[i];
            yield return new WaitForSeconds(textSpeed);
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

    [SerializeField]
    private List<dialogueCharacter> characters;

    [SerializeField]
    private float textSpeed = 0.25f;

    private GameObject dialogueBox;
    private TextMeshProUGUI dialogueTMP;

    short dialogueIndex;
}

[System.Serializable]
struct dialogueLine
{
    public string text;
    public bool isEnd;

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