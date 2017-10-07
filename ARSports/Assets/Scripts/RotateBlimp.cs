using UnityEngine;
using System.Collections;

public class RotateBlimp : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the object around its local X axis at 1 degree per second
        transform.Rotate(0, Time.deltaTime * -10, 0);
    }
}
