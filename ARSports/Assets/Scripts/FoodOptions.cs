using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodOptions : MonoBehaviour
{

    public static FoodOptions Instance { get; private set; }

    public List<GameObject> allSelections;

    // Use this for initialization
    void Start()
    {

        Instance = this;
        HideSelections();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HideSelections()
    {

        for (var i = 0; i < allSelections.Count; i++)
        {
            allSelections[i].SetActive(false);
        }
    }

}
