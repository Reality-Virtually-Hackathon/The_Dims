using UnityEngine;
using System.Collections;

public class ListSelector : MonoBehaviour
{

    public GameObject ListContainer;

    // Use this for initialization
    void Start()
    {

        ListContainer.SetActive(false);
    }

    void OnSelect()
    {
        ListContainer.SetActive(!ListContainer.activeSelf);
    }

    void OnGazeEnter() { }

    void OnMouseDown()
    {
        ListContainer.SetActive(!ListContainer.activeSelf);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
