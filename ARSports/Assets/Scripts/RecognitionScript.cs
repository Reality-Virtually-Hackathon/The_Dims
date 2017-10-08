using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;

public class RecognitionScript : MonoBehaviour
{
    KeywordRecognizer keywordRecognizer;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
    // Use this for initialization
    void Start()
    {
        MapKeywords();
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

    void MapKeywords()
    {
        keywords.Add("i am hungry", () =>
        {
            GameLogic.Instance.ShowFoodOptions(true);
        });

        keywords.Add("hide food options", () =>
        {
            GameLogic.Instance.ShowFoodOptions(false);
        });

        keywords.Add("show game", () =>
        {
            GameLogic.Instance.ShowTVScreen(true);
        });

        keywords.Add("hide game", () =>
        {
            GameLogic.Instance.ShowTVScreen(false);
        });

        keywords.Add("play game", () =>
        {
            ScreenLogic.Instance.TogglePlayVideo();
        });

        keywords.Add("pause game", () =>
        {
            ScreenLogic.Instance.TogglePlayVideo();
        });

        keywords.Add("show field", () =>
        {
            GameLogic.Instance.ShowFootballField(true);
        });

        keywords.Add("hide field", () =>
        {
            GameLogic.Instance.ShowFootballField(false);
        });

        keywords.Add("show scoreboard", () =>
        {
            GameLogic.Instance.ShowScoreboard(true);
        });

        keywords.Add("hide scoreboard", () =>
        {
            GameLogic.Instance.ShowScoreboard(false);
        });
    }
}