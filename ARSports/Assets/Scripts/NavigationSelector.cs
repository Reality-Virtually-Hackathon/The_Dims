using UnityEngine;

public class NavigationSelector : MonoBehaviour
{
    private bool isActiveIcon = false;
    public string NavDirection = string.Empty;

    void Awake()
    {
        //this.OrganModel.SetActive(false);
    }

    // Called by GazeGestureManager when the user performs a Select gesture
    void OnSelect()
    {
        // If the sphere has no Rigidbody component, add one to enable physics.
        if (!this.GetComponent<Rigidbody>())
        {
            isActiveIcon = true;
        }
    }

    void OnGazeEnter() { }

    void OnMouseDown()
    {
        GameLogic.Instance.BrowseGamesList(NavDirection);
    }

    void Update()
    {
        if (isActiveIcon)
        {
            GameLogic.Instance.BrowseGamesList(NavDirection);
            isActiveIcon = false;
        }
    }
}
