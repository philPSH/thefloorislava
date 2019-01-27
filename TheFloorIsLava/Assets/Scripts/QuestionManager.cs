using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour {

    public enum Guardians
    {
        DAD,
        MUM
    }

    [System.Serializable]
    public struct Question
    {
        public string question;
        public string dadAnswer;
        public string mumAnswer;
    }
    public LevelGenerator levelGen;
    public Guardians talkingTo;

    public Question[] questions;

    public RectTransform childTextSpawnDad;
    public RectTransform childTextSpawnMum;
    public RectTransform mumTextSpawn;
    public RectTransform dadTextSpawn;

    private List<RectTransform> previousChildTexts = new List<RectTransform>();
    private List<RectTransform> previousGuardianTexts = new List<RectTransform>();


    public GameObject textBoxPrefabChild;
    public GameObject textBoxPrefabAdult;
    public float questionCooldown;

    public float timeTillParentResponse;
    public float textBubbleOffset;
    private float timeSinceLastQuestion;

    public Font dadFont;
    public Font mumFont;
    public Font angryFont;
    public Font childFont;

    public string dadYell;
    public string mumYell;

    public int exhaustionMinAmount;
    public int exhaustionMaxAmount;

    private int currentExhaustion = 5;
    private int totalExhaustion;
    public bool canAskQuestions;
    private bool finishedAskingQuestions = false;

	// Use this for initialization
	void Start ()
    {
	    	
	}
	
	// Update is called once per frame
	void Update () {
        timeSinceLastQuestion += Time.deltaTime;

        if (canAskQuestions && !finishedAskingQuestions && ((Input.GetKeyDown(KeyCode.Y) || Input.GetButtonDown("Why")) && timeSinceLastQuestion >= questionCooldown))
        {
            timeSinceLastQuestion = 0;
            switch (talkingTo)
            {
                case Guardians.DAD:
                    AskQuestion(dadTextSpawn, childTextSpawnDad, Guardians.DAD);
                    break;

                case Guardians.MUM:
                    AskQuestion(mumTextSpawn, childTextSpawnMum, Guardians.MUM);
                    break;
            }
        }
	}

    private void AskQuestion(RectTransform childTextSpawn, RectTransform guardianTextSpawn, Guardians guardian)
    {
        currentExhaustion--;
        
        GameObject childText = GameObject.Instantiate(textBoxPrefabChild, childTextSpawn);
        Question question = questions[UnityEngine.Random.Range(0, questions.Length)];
        AdjustSpeechBubbles(previousChildTexts, childText.GetComponent<RectTransform>());
        childText.GetComponentInChildren<Text>().text = question.question;
        childText.GetComponentInChildren<Text>().font = childFont;

        if (currentExhaustion <= 0)
        {
            StartCoroutine(Yell(guardianTextSpawn, guardian));
        }
        else
        {
            StartCoroutine(ParentQuestion(guardianTextSpawn, question, guardian));
        }
    }

    private IEnumerator Yell(RectTransform guardianTextSpawn, Guardians guardian)
    {
        yield return new WaitForSeconds(timeTillParentResponse);
        GameObject adultText = GameObject.Instantiate(textBoxPrefabAdult, guardianTextSpawn);
        AdjustSpeechBubbles(previousGuardianTexts, adultText.GetComponent<RectTransform>());
        adultText.GetComponentInChildren<Text>().text = guardian == Guardians.DAD ? dadYell : mumYell;
        adultText.GetComponentInChildren<Text>().font = angryFont;
        levelGen.GenerateLevel(totalExhaustion * 3, Mathf.Clamp(totalExhaustion, 10, 100));
        finishedAskingQuestions = true;
    }

    private void AdjustSpeechBubbles(List<RectTransform> bubbles, RectTransform newText)
    {
        foreach (RectTransform text in bubbles)
        {
            text.position = new Vector3(text.position.x, text.position.y + textBubbleOffset, text.position.z);
        }
        if (bubbles.Count > 10) bubbles.Remove(bubbles[10]);
        previousChildTexts.Add(newText);
    }

    private IEnumerator ParentQuestion(RectTransform guardianTextSpawn, Question question, Guardians guardian)
    {
        yield return new WaitForSeconds(timeTillParentResponse);
        GameObject adultText = GameObject.Instantiate(textBoxPrefabAdult, guardianTextSpawn);
        AdjustSpeechBubbles(previousGuardianTexts, adultText.GetComponent<RectTransform>());
        adultText.GetComponentInChildren<Text>().text = guardian == Guardians.DAD ? question.dadAnswer : question.mumAnswer;
        adultText.GetComponentInChildren<Text>().font = guardian == Guardians.DAD ? dadFont : mumFont;
    }

    public void EnteredTrigger(Guardians triggerGuardian)
    {
        canAskQuestions = true;
        if (talkingTo == triggerGuardian) return;
        currentExhaustion = UnityEngine.Random.Range(exhaustionMinAmount, exhaustionMaxAmount);
        totalExhaustion += currentExhaustion;
        talkingTo = triggerGuardian;
        finishedAskingQuestions = false;
    }

    public void ExitTrigger()
    {
        canAskQuestions = false;
    }
}
