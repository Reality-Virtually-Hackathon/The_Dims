using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListGameSelector : MonoBehaviour {

    public GameObject FootballFieldRef;

	// Use this for initialization
	void Start () {
        FootballFieldRef.SetActive(false);
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
        FootballFieldRef.SetActive(!FootballFieldRef.activeSelf);
    }

    void OnMouseDown()
    {
        FootballFieldRef.SetActive(!FootballFieldRef.activeSelf);
    }
}
