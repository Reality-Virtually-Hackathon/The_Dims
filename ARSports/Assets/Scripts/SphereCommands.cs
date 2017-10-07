using UnityEngine;

public class SphereCommands : MonoBehaviour
{
    private bool isActiveIcon = false;
    //public GameObject OrganModel;

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

    void OnMouseDown()
    {
        GameLogic.Instance.UpdateOtherTeam();
    }

    void Update()
    {
        if (isActiveIcon)
        {
            GameLogic.Instance.UpdateOtherTeam();
            isActiveIcon = false;
        }
    }
}
