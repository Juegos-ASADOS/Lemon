using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class DialogueSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        dialogueBox = transform.Find("TextBox").gameObject;
        dialogueTMP = dialogueBox.transform.Find("DialogueText").GetComponent<TextMeshProUGUI>();
        StartCoroutine(dialogueStart());
    }

    // Update is called once per frame
    void Update()
    {
        //Listen to event
    }

    public IEnumerator dialogueStart()
    {

        dialogueBox.SetActive(true);

        // Get characterEvent
        foreach (dialogueCharacter dC in characters)
        {
            yield return StartCoroutine(PrintDialogue(dC.dialogueList));
        }
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
        //dialogueStop();
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
    //Textos de momento aqui
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