using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ScreenLogic : MonoBehaviour
{

    public VideoPlayer mainScreenPlayer;
    public GameObject mute;
    public GameObject unmute;
    public GameObject playSymbol;
    public GameObject pauseSymbol;

    private bool isPlaying = true;

    public static ScreenLogic Instance { get; private set; }

    // Use this for initialization
    void Start()
    {
        Instance = this;
        pauseSymbol.SetActive(false);
        mainScreenPlayer.Pause();
        mute.SetActive(false);
        unmute.SetActive(true);
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
        Debug.Log("forward");
        mainScreenPlayer.StepForward();
        mainScreenPlayer.Play();
    }

    public void ToggleMuteVideo()
    {
        isPlaying = !isPlaying;

        if (isPlaying)
        {

            mainScreenPlayer.SetDirectAudioMute(0, false);
            mute.SetActive(false);
            unmute.SetActive(true);
            Debug.Log("true");
        }
        else
        {
            mainScreenPlayer.SetDirectAudioMute(0, true);
            mute.SetActive(true);
            unmute.SetActive(false);
            Debug.Log("false");
        }
    }
}
