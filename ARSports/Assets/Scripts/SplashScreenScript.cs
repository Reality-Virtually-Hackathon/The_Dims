using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreenScript : MonoBehaviour {

    public GameObject dot1;
    public GameObject dot2;
    public GameObject dot3;

	// Use this for initialization
	void Start () {

        dot1.SetActive(false);
        dot2.SetActive(false);
        dot3.SetActive(false);

        LoadingAnimation();
        //InvokeRepeating("LoadingAnimation", 1f, 1f);
        Invoke("LaunchMainScene", 5f);
    }

   
	
	// Update is called once per frame
	void Update () {

        StartCoroutine(LoadingAnimation());
	}

    void DoSomething() { }


    void LaunchMainScene()
    {
        Application.LoadLevel("ARSportsMainScene");
    }

    IEnumerator LoadingAnimation()
    {
        if (!dot1.activeSelf)
        {
            dot1.SetActive(true);
            yield return new WaitForSeconds(1f);
        }

        if (dot1.activeSelf)
        {
            dot2.SetActive(true);
            yield return new WaitForSeconds(1f);
        }

        if (dot2.activeSelf)
        {
            dot3.SetActive(true);
            yield return new WaitForSeconds(1f);
        }

        if (dot3.activeSelf)
        {
            dot1.SetActive(false);
            dot2.SetActive(false);
            dot3.SetActive(false);
            yield return new WaitForSeconds(1f);
        }
        
    }
}
