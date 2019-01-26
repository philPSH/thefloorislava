using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianTrigger : MonoBehaviour {
    public QuestionManager questionManager;
    public QuestionManager.Guardians guardian;
	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            questionManager.EnteredTrigger(guardian);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            questionManager.ExitTrigger();
        }
    }
}
