using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianTrigger : MonoBehaviour {
    public QuestionManager questionManager;
    public QuestionManager.Guardians guardian;
	// Use this for initialization
	void Start () {
        questionManager = GameObject.Find("QuestionManager").GetComponent<QuestionManager>();
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            questionManager.EnteredTrigger(guardian);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            questionManager.ExitTrigger();
        }
    }
}
