using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ScreenLogic : MonoBehaviour
{

    public VideoPlayer mainScreenPlayer;
    public GameObject muteSlash;
    public GameObject playSymbol;
    public GameObject pauseSymbol;

    private bool isPlaying = true;

    public static ScreenLogic Instance { get; private set; }

    // Use this for initialization
    void Start()
    {

        Instance = this;
        pauseSymbol.SetActive(false);
        muteSlash.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TogglePlayVideo()
    {

        if (mainScreenPlayer.isPlaying)
        {
            mainScreenPlayer.Pause();
            playSymbol.SetActive(false);
            pauseSymbol.SetActive(true);
        }
        else
        {
            mainScreenPlayer.Play();
            playSymbol.SetActive(true);
            pauseSymbol.SetActive(false);
        }
    }

    public void ForwardVideo()
    {
        mainScreenPlayer.StepForward();
        mainScreenPlayer.Play();
    }

    public void ToggleMuteVideo()
    {
        if (mainScreenPlayer.GetDirectAudioMute(0))
        {
            mainScreenPlayer.SetDirectAudioMute(0, false);
            muteSlash.SetActive(false);
        }
        else
        {
            mainScreenPlayer.SetDirectAudioMute(0, true);
            muteSlash.SetActive(true);
        }
    }
}
