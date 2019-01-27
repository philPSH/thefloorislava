using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUp : MonoBehaviour {
    public float speed;
    private RectTransform rect;
	// Use this for initialization
	void Start () {
        rect = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        rect.position = new Vector3(rect.position.x, rect.position.y + speed, rect.position.z);
	}
}
