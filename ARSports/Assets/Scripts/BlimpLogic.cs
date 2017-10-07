using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlimpLogic : MonoBehaviour
{

    public GameObject blimpObject;
    public List<Material> blimpMaterials;
    public bool isDefaultApplied = true;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnSelect()
    {
        // If the sphere has no Rigidbody component, add one to enable physics.
        if (!this.GetComponent<Rigidbody>())
        {
            SwitchBlimpMaterial();
        }
    }

    void OnMouseDown()
    {
        SwitchBlimpMaterial();
    }

    void SwitchBlimpMaterial()
    {
        this.isDefaultApplied = !this.isDefaultApplied;
        blimpObject.GetComponent<Renderer>().sharedMaterial = blimpMaterials[(this.isDefaultApplied ? 0 : 1)];
    }
}
