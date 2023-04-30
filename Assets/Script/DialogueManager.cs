using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject[] choices;
    [SerializeField] private PlayerHandler Player;
    private TextMeshProUGUI[] choicesText;

    private Story currenStory;
    private bool dialogueIsPlaying;

    private static DialogueManager instance;
    private void Awake()
    {
        if(instance != null)
        {

        }
        instance = this;
    }
    public static DialogueManager GetInstance()
    {
        return instance;
    }
    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach(GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }
    private void Update()
    {
        if (!dialogueIsPlaying)
        {
            return;
        }
        if (currenStory.currentChoices.Count == 0&& Input.GetMouseButtonDown(0))
        {
            ContinueStory();
        }
    }
    public void EnterDiaalogueMode(TextAsset inkJSON)
    {
        Cursor.visible = true;
        Player.CanMove = false;
        currenStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        ContinueStory();


    }
    private void ExitDialogueMode()
    {
        Cursor.visible = false;
        Player.CanMove = true;
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }
    private void ContinueStory()
    {
        if (currenStory.canContinue)
        {
            dialogueText.text = currenStory.Continue();
            DisplayChoices();
        }
        else
        {
            ExitDialogueMode();
        }
    }
    private void DisplayChoices()
    {
        List<Choice> currenChoice = currenStory.currentChoices;
        int index = 0;
        foreach(Choice choice in currenChoice)
        {
            Cursor.visible = true;
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
        StartCoroutine(SelectFirstChoice());
    }
    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }
    public void MakeChoice(int choiceIndex)
    {
        currenStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();

    }
}
