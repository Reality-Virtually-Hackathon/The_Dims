using UnityEngine;

public class RowSelection : MonoBehaviour
{
    public bool isActiveIcon = false;
    public int Row = 0;
    public Material[] SelectionMaterials;
    public GameObject RowObject;

    void Awake()
    {
        //this.OrganModel.SetActive(false);
    }

    public void OnResetActiveIcon()
    {
        isActiveIcon = false;
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
        // If the sphere has no Rigidbody component, add one to enable physics.
        if (!this.GetComponent<Rigidbody>())
        {
            OnRowSelection();
        }
    }

    void OnMouseDown()
    {
        OnRowSelection();
    }

    void OnRowSelection()
    {
        GameLogic.Instance.ResetRows();
        isActiveIcon = true;
        GameLogic.Instance.SelectRow(Row);
    }

    void Update()
    {
        RowObject.GetComponent<Renderer>().sharedMaterial = isActiveIcon ? SelectionMaterials[1] : SelectionMaterials[0];
    }
}
