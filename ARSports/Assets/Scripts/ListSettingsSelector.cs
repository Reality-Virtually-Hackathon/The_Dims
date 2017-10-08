using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListSettingsSelector : MonoBehaviour {

    public GameObject tvRef;
	// Use this for initialization
	void Start () {
        tvRef.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnGazeEnter()
    {
    }

    void OnGazeExit()
    {
    }

    void OnSelect()
    {
        tvRef.SetActive(!tvRef.activeSelf);
    }

    void OnMouseDown()
    {
        tvRef.SetActive(!tvRef.activeSelf);
    }
}
