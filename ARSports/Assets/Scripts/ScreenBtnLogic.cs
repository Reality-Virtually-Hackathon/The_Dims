using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBtnLogic : MonoBehaviour
{

    public string BtnTag;

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
            // same as on mouse down
        }
    }

    void OnMouseDown()
    {
        switch (this.BtnTag)
        {
            case "PLAY_BTN":
                ScreenLogic.Instance.TogglePlayVideo();
                break;
            case "FWD_BTN":
                ScreenLogic.Instance.ForwardVideo();
                break;
            case "MUTE_BTN":
                ScreenLogic.Instance.ToggleMuteVideo();
                break;
        }
    }
}
