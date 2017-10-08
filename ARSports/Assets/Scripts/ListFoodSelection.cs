using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListFoodSelection : MonoBehaviour {

    public GameObject foodOptions;

	// Use this for initialization
	void Start () {
        foodOptions.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnSelect()
    {
        foodOptions.SetActive(!foodOptions.activeSelf);
    }

    void OnGazeEnter()
    {
    }

    void OnGazeExit()
    {
    }

    void OnMouseDown()
    {
        foodOptions.SetActive(!foodOptions.activeSelf);
    }
}
