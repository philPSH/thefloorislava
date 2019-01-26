using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs = null;
    private Transform baseMarker = null;
    private GameObject obj = null;

	void Start () {
        // find marker which inidcates column's base
        baseMarker = this.gameObject.transform.GetChild(0);

        // assign random prefab
        obj = prefabs[Random.Range(0, prefabs.Length)];

        // instantiate prefab
        Instantiate(obj, baseMarker, false);
	}
}
