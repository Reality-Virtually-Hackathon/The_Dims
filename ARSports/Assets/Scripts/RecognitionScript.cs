using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;

public class RecognitionScript : MonoBehaviour
{
    public GameObject foodMenu;

    KeywordRecognizer keywordRecognizer;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
    // Use this for initialization
    void Start()
    {
        foodMenu.SetActive(false);

        keywords.Add("i am hungry", () =>
        {
            //HeyCalled();
            foodMenu.SetActive(true);
        });

        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizerOnPhraseRecognized;
        keywordRecognizer.Start();
    }
    void KeywordRecognizerOnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }
    void HeyCalled()
    {
        print("you just said HELLO");
    }
    // Update is called once per frame
    void Update()
    {

    }
}