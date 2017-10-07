using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodOptionsSelection : MonoBehaviour
{

    public GameObject SelectionSquare;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseEnter()
    {
        FoodOptions.Instance.HideSelections();
        this.SelectionSquare.SetActive(true);
    }

    void OnMouseExit()
    {
        FoodOptions.Instance.HideSelections();
    }
}
