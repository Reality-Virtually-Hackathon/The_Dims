using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballScript : MonoBehaviour
{
    public GameObject footballTooltip;

    // Use this for initialization
    void Start()
    {
        footballTooltip.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGazeEnter()
    {
        // OnRowSelection();
    }

    void OnGazeExit()
    {
        // GameLogic.Instance.ResetRows();
    }

    // Called by GazeGestureManager when the user performs a Select gesture
    void OnSelect()
    {
        footballTooltip.SetActive(footballTooltip.activeSelf ? false : true);
    }

    void OnMouseDown()
    {
        footballTooltip.SetActive(footballTooltip.activeSelf ? false : true);
    }
}
