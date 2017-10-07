using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.Linq;
using System.IO;
using UnityEngine.Video;

public class GameLogic : MonoBehaviour {

    public GameObject leftHelmet;
    public GameObject rightHelmet;
    public GameObject leftHelmetFace;
    public GameObject rightHelmetFace;
    public GameObject leftHelmetFastener;
    public GameObject rightHelmetFastener;

    public GameObject homeTeamNameLabel;
    public GameObject guestTeamNameLabel;
    public GameObject homeTeamScoreLabel;
    public GameObject guestTeamScoreLabel;

    public GameObject blimp;

    public GameObject homeTeamBanner;
    public GameObject homeTeamScoreboardLogo;
    public GameObject guestTeamScoreboardLogo;
    public GameObject guestTeamBanner;
    public GameObject mainField;
    public GameObject listRow1;
    public GameObject listRow2;
	public GameObject listRow3;

    public Material[] helmets;
    public Material[] helmetFaces;
    public Material[] helmetFasteners;
    public Material[] blimps;
    public Material[] teamBanners;
    public Material[] fieldThemes;
    public Material[] goalPostsThemes;
    public Material[] scoreboardTeamLogos;
    public Material[] selectionMaterials;

    public Venue venue;
    public Team homeTeam;
    public Team guestTeam;
    public TeamMetadata homeTeamMetadata;
    public TeamMetadata guestTeamMetadata;
    public Dictionary<string, TeamMetadata> allTeams;
    public List<GameMatch> allMatches;

	public List<VideoClip> superBowlClips;
	public VideoPlayer mainScreenPlayer;

    public static GameLogic Instance { get; private set; }

    private bool swichTeams = false;

    // Use this for initialization
    void Start() {

        Instance = this;

        SetupTeamMetadata();

        //string url = "http://dumpster.drcoderz.com/sampledata.json";
        //WWW www = new WWW(url);
       // yield return www;

        //ProcessJSON(www.text);


        //Payload p = ProcessJSON(www.text);

        //leftHelmet.GetComponent<Renderer>().sharedMaterial = helmets[p.HomeTeam.TeamIndex];
        //rightHelmet.GetComponent<Renderer>().sharedMaterial = helmets[p.GuestTeam.TeamIndex];
        //leftHelmetFace.GetComponent<Renderer>().sharedMaterial = helmetFaces[p.HomeTeam.TeamIndex];
        //rightHelmetFace.GetComponent<Renderer>().sharedMaterial = helmetFaces[p.GuestTeam.TeamIndex];
        //leftHelmetFastener.GetComponent<Renderer>().sharedMaterial = helmetFasteners[p.HomeTeam.TeamIndex];
        //rightHelmetFastener.GetComponent<Renderer>().sharedMaterial = helmetFasteners[p.GuestTeam.TeamIndex];
        //homeTeamBanner.GetComponent<Renderer>().sharedMaterial = teamBanners[p.HomeTeam.TeamIndex];
        //guestTeamBanner.GetComponent<Renderer>().sharedMaterial = teamBanners[p.GuestTeam.TeamIndex];
        //homeTeamScoreboardLogo.GetComponent<Renderer>().sharedMaterial = scoreboardTeamLogos[p.HomeTeam.TeamIndex];
        //guestTeamScoreboardLogo.GetComponent<Renderer>().sharedMaterial = scoreboardTeamLogos[p.GuestTeam.TeamIndex];

        blimp.GetComponent<Renderer>().sharedMaterial = blimps[0];
        mainField.GetComponent<Renderer>().sharedMaterial = fieldThemes[0];

        //InvokeRepeating("UpdateScores", 0, 3);
        //UpdateScores();
    }

    void ProcessJSON(string jsonString)
    {
        var dic = Json.Deserialize(jsonString) as Dictionary<string, object>;

        var jsonSummary = dic["summary"] as Dictionary<string, object>;
        var jsonVenue = jsonSummary["venue"] as Dictionary<string, object>;
        var jsonHomeTeam = jsonSummary["home"] as Dictionary<string, object>;
        var jsonGuestTeam = jsonSummary["away"] as Dictionary<string, object>;

        venue = new Venue(jsonVenue);
        homeTeam = new Team(jsonHomeTeam);
        guestTeam = new Team(jsonGuestTeam);

        homeTeamMetadata = allTeams[homeTeam.alias];
        guestTeamMetadata = allTeams[guestTeam.alias];
    }

    void SetupTeamMetadata()
    {
        allTeams = new Dictionary<string, TeamMetadata>();

        List<string> allTeamsAliases = new List<string>()
        {
             "GBY",
             "MIA",
             "SFO",
             "DEN",
             "ATL",
             "NWE"
        };

        for (int i = 0; i < allTeamsAliases.Count; i++)
        {
            allTeams.Add(allTeamsAliases[i], new TeamMetadata(i, helmets, helmetFaces, helmetFasteners, teamBanners, scoreboardTeamLogos));
        }

        allMatches = new List<GameMatch>
        {
            new GameMatch { HomeTeam = new Team { alias = "GBY", points = "10" }, GuestTeam = new Team { alias = "MIA", points = "28" } },
            new GameMatch { HomeTeam = new Team { alias = "SFO", points = "15" }, GuestTeam = new Team { alias = "DEN", points = "18" } },
            new GameMatch { HomeTeam = new Team { alias = "ATL", points = "14" }, GuestTeam = new Team { alias = "NWE", points = "0" } } ,
            new GameMatch { HomeTeam = new Team { alias = "NYG", points = "16" }, GuestTeam = new Team { alias = "NYJ", points = "32" } },
            new GameMatch { HomeTeam = new Team { alias = "MIN", points = "45" }, GuestTeam = new Team { alias = "NWE", points = "20" } }
        };
    }

    public void ResetRows()
    {
        listRow1.GetComponent<Renderer>().sharedMaterial = selectionMaterials[0];
        listRow2.GetComponent<Renderer>().sharedMaterial = selectionMaterials[0];
		listRow3.GetComponent<Renderer> ().sharedMaterial = selectionMaterials[0];

        RowSelection list1Script = listRow1.GetComponent<RowSelection>();
        list1Script.OnResetActiveIcon();

        RowSelection list2Script = listRow2.GetComponent<RowSelection>();
        list2Script.OnResetActiveIcon();

		RowSelection list3Script = listRow3.GetComponent<RowSelection>();
		list3Script.OnResetActiveIcon();
    }

    public void UpdateOtherTeam()
    {
        
    }

    public void BrowseGamesList(string direction)
    {

    }

    public void SelectRow(int rowNum)
    {
        GameMatch match = allMatches[rowNum];

        homeTeam = match.HomeTeam;
        guestTeam = match.GuestTeam;

        homeTeamMetadata = allTeams[homeTeam.alias];
        guestTeamMetadata = allTeams[guestTeam.alias];

        homeTeamScoreLabel.GetComponent<TextMesh>().text = homeTeam.points.ToString();
        guestTeamScoreLabel.GetComponent<TextMesh>().text = guestTeam.points.ToString();

        homeTeamNameLabel.GetComponent<TextMesh>().text = homeTeam.alias;
        guestTeamNameLabel.GetComponent<TextMesh>().text = guestTeam.alias;

        leftHelmet.GetComponent<Renderer>().sharedMaterial = homeTeamMetadata.helmet;
        rightHelmet.GetComponent<Renderer>().sharedMaterial = guestTeamMetadata.helmet;

        leftHelmetFace.GetComponent<Renderer>().sharedMaterial = homeTeamMetadata.helmetRail;
        rightHelmetFace.GetComponent<Renderer>().sharedMaterial = guestTeamMetadata.helmetRail;

        leftHelmetFastener.GetComponent<Renderer>().sharedMaterial = homeTeamMetadata.fasteners;
        rightHelmetFastener.GetComponent<Renderer>().sharedMaterial = guestTeamMetadata.fasteners;

        homeTeamBanner.GetComponent<Renderer>().sharedMaterial = homeTeamMetadata.teamBanner;
        guestTeamBanner.GetComponent<Renderer>().sharedMaterial = guestTeamMetadata.teamBanner;

        homeTeamScoreboardLogo.GetComponent<Renderer>().sharedMaterial = homeTeamMetadata.scoreboardLogo;
        guestTeamScoreboardLogo.GetComponent<Renderer>().sharedMaterial = guestTeamMetadata.scoreboardLogo;

		mainScreenPlayer.clip = superBowlClips [rowNum];
    }

    //Payload ProcessJSON(string jsonString)
    //{
    //    Payload p = new Payload(jsonString);
    //    return p;
    //}

    // Update is called once per frame
    void Update() {
    }

    public class Team
    {
        public string name { get; set; }
        public string market { get; set; }
        public string alias { get; set; }
        public string points { get; set; }

        public Team() { }

        public Team(Dictionary<string, object> jsonData)
        {
            name = (string)jsonData["name"];
            market = (string)jsonData["market"];
            alias = (string)jsonData["alias"];
            points = jsonData["points"].ToString();
        }
    }

    public class Venue
    {
        public string name { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string zip { get; set; }
        public string address { get; set; }

        public Venue(Dictionary<string, object> jsonData)
        {
            name = (string)jsonData["name"];
            city = (string)jsonData["city"];
            state = (string)jsonData["state"];
            country = (string)jsonData["country"];
            zip = (string)jsonData["zip"];
            address = (string)jsonData["address"];
        }
    }

    public class GameMatch
    {
        public Team HomeTeam { get; set; }
        public Team GuestTeam { get; set; }
    }

    public class TeamMetadata
    {
        public Material helmet;
        public Material helmetRail;
        public Material fasteners;
        public Material teamBanner;
        public Material scoreboardLogo;

        public TeamMetadata(int index, Material[] helmets, Material[] helmetFaces, Material[] helmetFasteners, Material[] teamBanners, Material[] scoreboardTeamLogos)
        {
            helmet = helmets[index];
            helmetRail = helmetFaces[index];
            fasteners = helmetFasteners[index];
            teamBanner = teamBanners[index];
            scoreboardLogo = scoreboardTeamLogos[index];
        }
    }

    public enum TeamType
    {
        HomeTeam = 0,
        GuestTeam = 1
    }

    /*
 * Copyright (c) 2013 Calvin Rien
 *
 * Based on the JSON parser by Patrick van Bergen
 * http://techblog.procurios.nl/k/618/news/view/14605/14863/How-do-I-write-my-own-parser-for-JSON.html
 *
 * Simplified it so that it doesn't throw exceptions
 * and can be used in Unity iPhone with maximum code stripping.
 *
 * Permission is hereby granted, free of charge, to any person obtaining
 * a copy of this software and associated documentation files (the
 * "Software"), to deal in the Software without restriction, including
 * without limitation the rights to use, copy, modify, merge, publish,
 * distribute, sublicense, and/or sell copies of the Software, and to
 * permit persons to whom the Software is furnished to do so, subject to
 * the following conditions:
 *
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
 * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
 * CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
 * TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
 * SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

    // Example usage:
    //
    //  using UnityEngine;
    //  using System.Collections;
    //  using System.Collections.Generic;
    //  using MiniJSON;
    //
    //  public class MiniJSONTest : MonoBehaviour {
    //      void Start () {
    //          var jsonString = "{ \"array\": [1.44,2,3], " +
    //                          "\"object\": {\"key1\":\"value1\", \"key2\":256}, " +
    //                          "\"string\": \"The quick brown fox \\\"jumps\\\" over the lazy dog \", " +
    //                          "\"unicode\": \"\\u3041 Men\u00fa sesi\u00f3n\", " +
    //                          "\"int\": 65536, " +
    //                          "\"float\": 3.1415926, " +
    //                          "\"bool\": true, " +
    //                          "\"null\": null }";
    //
    //          var dict = Json.Deserialize(jsonString) as Dictionary<string,object>;
    //
    //          Debug.Log("deserialized: " + dict.GetType());
    //          Debug.Log("dict['array'][0]: " + ((List<object>) dict["array"])[0]);
    //          Debug.Log("dict['string']: " + (string) dict["string"]);
    //          Debug.Log("dict['float']: " + (double) dict["float"]); // floats come out as doubles
    //          Debug.Log("dict['int']: " + (long) dict["int"]); // ints come out as longs
    //          Debug.Log("dict['unicode']: " + (string) dict["unicode"]);
    //
    //          var str = Json.Serialize(dict);
    //
    //          Debug.Log("serialized: " + str);
    //      }
    //  }

    /// <summary>
    /// This class encodes and decodes JSON strings.
    /// Spec. details, see http://www.json.org/
    ///
    /// JSON uses Arrays and Objects. These correspond here to the datatypes IList and IDictionary.
    /// All numbers are parsed to doubles.
    /// </summary>
    public static class Json
    {
        /// <summary>
        /// Parses the string json into a value
        /// </summary>
        /// <param name="json">A JSON string.</param>
        /// <returns>An List&lt;object&gt;, a Dictionary&lt;string, object&gt;, a double, an integer,a string, null, true, or false</returns>
        public static object Deserialize(string json)
        {
            // save the string for debug information
            if (json == null)
            {
                return null;
            }

            return Parser.Parse(json);
        }

        sealed class Parser : IDisposable
        {
            const string WORD_BREAK = "{}[],:\"";

            public static bool IsWordBreak(char c)
            {
                return Char.IsWhiteSpace(c) || WORD_BREAK.IndexOf(c) != -1;
            }

            enum TOKEN
            {
                NONE,
                CURLY_OPEN,
                CURLY_CLOSE,
                SQUARED_OPEN,
                SQUARED_CLOSE,
                COLON,
                COMMA,
                STRING,
                NUMBER,
                TRUE,
                FALSE,
                NULL
            };

            StringReader json;

            Parser(string jsonString)
            {
                json = new StringReader(jsonString);
            }

            public static object Parse(string jsonString)
            {
                using (var instance = new Parser(jsonString))
                {
                    return instance.ParseValue();
                }
            }

            public void Dispose()
            {
                json.Dispose();
                json = null;
            }

            Dictionary<string, object> ParseObject()
            {
                Dictionary<string, object> table = new Dictionary<string, object>();

                // ditch opening brace
                json.Read();

                // {
                while (true)
                {
                    switch (NextToken)
                    {
                        case TOKEN.NONE:
                            return null;
                        case TOKEN.COMMA:
                            continue;
                        case TOKEN.CURLY_CLOSE:
                            return table;
                        default:
                            // name
                            string name = ParseString();
                            if (name == null)
                            {
                                return null;
                            }

                            // :
                            if (NextToken != TOKEN.COLON)
                            {
                                return null;
                            }
                            // ditch the colon
                            json.Read();

                            // value
                            table[name] = ParseValue();
                            break;
                    }
                }
            }

            List<object> ParseArray()
            {
                List<object> array = new List<object>();

                // ditch opening bracket
                json.Read();

                // [
                var parsing = true;
                while (parsing)
                {
                    TOKEN nextToken = NextToken;

                    switch (nextToken)
                    {
                        case TOKEN.NONE:
                            return null;
                        case TOKEN.COMMA:
                            continue;
                        case TOKEN.SQUARED_CLOSE:
                            parsing = false;
                            break;
                        default:
                            object value = ParseByToken(nextToken);

                            array.Add(value);
                            break;
                    }
                }

                return array;
            }

            object ParseValue()
            {
                TOKEN nextToken = NextToken;
                return ParseByToken(nextToken);
            }

            object ParseByToken(TOKEN token)
            {
                switch (token)
                {
                    case TOKEN.STRING:
                        return ParseString();
                    case TOKEN.NUMBER:
                        return ParseNumber();
                    case TOKEN.CURLY_OPEN:
                        return ParseObject();
                    case TOKEN.SQUARED_OPEN:
                        return ParseArray();
                    case TOKEN.TRUE:
                        return true;
                    case TOKEN.FALSE:
                        return false;
                    case TOKEN.NULL:
                        return null;
                    default:
                        return null;
                }
            }

            string ParseString()
            {
                StringBuilder s = new StringBuilder();
                char c;

                // ditch opening quote
                json.Read();

                bool parsing = true;
                while (parsing)
                {

                    if (json.Peek() == -1)
                    {
                        parsing = false;
                        break;
                    }

                    c = NextChar;
                    switch (c)
                    {
                        case '"':
                            parsing = false;
                            break;
                        case '\\':
                            if (json.Peek() == -1)
                            {
                                parsing = false;
                                break;
                            }

                            c = NextChar;
                            switch (c)
                            {
                                case '"':
                                case '\\':
                                case '/':
                                    s.Append(c);
                                    break;
                                case 'b':
                                    s.Append('\b');
                                    break;
                                case 'f':
                                    s.Append('\f');
                                    break;
                                case 'n':
                                    s.Append('\n');
                                    break;
                                case 'r':
                                    s.Append('\r');
                                    break;
                                case 't':
                                    s.Append('\t');
                                    break;
                                case 'u':
                                    var hex = new char[4];

                                    for (int i = 0; i < 4; i++)
                                    {
                                        hex[i] = NextChar;
                                    }

                                    s.Append((char)Convert.ToInt32(new string(hex), 16));
                                    break;
                            }
                            break;
                        default:
                            s.Append(c);
                            break;
                    }
                }

                return s.ToString();
            }

            object ParseNumber()
            {
                string number = NextWord;

                if (number.IndexOf('.') == -1)
                {
                    long parsedInt;
                    Int64.TryParse(number, out parsedInt);
                    return parsedInt;
                }

                double parsedDouble;
                Double.TryParse(number, out parsedDouble);
                return parsedDouble;
            }

            void EatWhitespace()
            {
                while (Char.IsWhiteSpace(PeekChar))
                {
                    json.Read();

                    if (json.Peek() == -1)
                    {
                        break;
                    }
                }
            }

            char PeekChar
            {
                get
                {
                    return Convert.ToChar(json.Peek());
                }
            }

            char NextChar
            {
                get
                {
                    return Convert.ToChar(json.Read());
                }
            }

            string NextWord
            {
                get
                {
                    StringBuilder word = new StringBuilder();

                    while (!IsWordBreak(PeekChar))
                    {
                        word.Append(NextChar);

                        if (json.Peek() == -1)
                        {
                            break;
                        }
                    }

                    return word.ToString();
                }
            }

            TOKEN NextToken
            {
                get
                {
                    EatWhitespace();

                    if (json.Peek() == -1)
                    {
                        return TOKEN.NONE;
                    }

                    switch (PeekChar)
                    {
                        case '{':
                            return TOKEN.CURLY_OPEN;
                        case '}':
                            json.Read();
                            return TOKEN.CURLY_CLOSE;
                        case '[':
                            return TOKEN.SQUARED_OPEN;
                        case ']':
                            json.Read();
                            return TOKEN.SQUARED_CLOSE;
                        case ',':
                            json.Read();
                            return TOKEN.COMMA;
                        case '"':
                            return TOKEN.STRING;
                        case ':':
                            return TOKEN.COLON;
                        case '0':
                        case '1':
                        case '2':
                        case '3':
                        case '4':
                        case '5':
                        case '6':
                        case '7':
                        case '8':
                        case '9':
                        case '-':
                            return TOKEN.NUMBER;
                    }

                    switch (NextWord)
                    {
                        case "false":
                            return TOKEN.FALSE;
                        case "true":
                            return TOKEN.TRUE;
                        case "null":
                            return TOKEN.NULL;
                    }

                    return TOKEN.NONE;
                }
            }
        }

        /// <summary>
        /// Converts a IDictionary / IList object or a simple type (string, int, etc.) into a JSON string
        /// </summary>
        /// <param name="json">A Dictionary&lt;string, object&gt; / List&lt;object&gt;</param>
        /// <returns>A JSON encoded string, or null if object 'json' is not serializable</returns>
        public static string Serialize(object obj)
        {
            return Serializer.Serialize(obj);
        }

        sealed class Serializer
        {
            StringBuilder builder;

            Serializer()
            {
                builder = new StringBuilder();
            }

            public static string Serialize(object obj)
            {
                var instance = new Serializer();

                instance.SerializeValue(obj);

                return instance.builder.ToString();
            }

            void SerializeValue(object value)
            {
                IList asList;
                IDictionary asDict;
                string asStr;

                if (value == null)
                {
                    builder.Append("null");
                }
                else if ((asStr = value as string) != null)
                {
                    SerializeString(asStr);
                }
                else if (value is bool)
                {
                    builder.Append((bool)value ? "true" : "false");
                }
                else if ((asList = value as IList) != null)
                {
                    SerializeArray(asList);
                }
                else if ((asDict = value as IDictionary) != null)
                {
                    SerializeObject(asDict);
                }
                else if (value is char)
                {
                    SerializeString(new string((char)value, 1));
                }
                else
                {
                    SerializeOther(value);
                }
            }

            void SerializeObject(IDictionary obj)
            {
                bool first = true;

                builder.Append('{');

                foreach (object e in obj.Keys)
                {
                    if (!first)
                    {
                        builder.Append(',');
                    }

                    SerializeString(e.ToString());
                    builder.Append(':');

                    SerializeValue(obj[e]);

                    first = false;
                }

                builder.Append('}');
            }

            void SerializeArray(IList anArray)
            {
                builder.Append('[');

                bool first = true;

                foreach (object obj in anArray)
                {
                    if (!first)
                    {
                        builder.Append(',');
                    }

                    SerializeValue(obj);

                    first = false;
                }

                builder.Append(']');
            }

            void SerializeString(string str)
            {
                builder.Append('\"');

                char[] charArray = str.ToCharArray();
                foreach (var c in charArray)
                {
                    switch (c)
                    {
                        case '"':
                            builder.Append("\\\"");
                            break;
                        case '\\':
                            builder.Append("\\\\");
                            break;
                        case '\b':
                            builder.Append("\\b");
                            break;
                        case '\f':
                            builder.Append("\\f");
                            break;
                        case '\n':
                            builder.Append("\\n");
                            break;
                        case '\r':
                            builder.Append("\\r");
                            break;
                        case '\t':
                            builder.Append("\\t");
                            break;
                        default:
                            int codepoint = Convert.ToInt32(c);
                            if ((codepoint >= 32) && (codepoint <= 126))
                            {
                                builder.Append(c);
                            }
                            else
                            {
                                builder.Append("\\u");
                                builder.Append(codepoint.ToString("x4"));
                            }
                            break;
                    }
                }

                builder.Append('\"');
            }

            void SerializeOther(object value)
            {
                // NOTE: decimals lose precision during serialization.
                // They always have, I'm just letting you know.
                // Previously floats and doubles lost precision too.
                if (value is float)
                {
                    builder.Append(((float)value).ToString("R"));
                }
                else if (value is int
                  || value is uint
                  || value is long
                  || value is sbyte
                  || value is byte
                  || value is short
                  || value is ushort
                  || value is ulong)
                {
                    builder.Append(value);
                }
                else if (value is double
                  || value is decimal)
                {
                    builder.Append(Convert.ToDouble(value).ToString("R"));
                }
                else
                {
                    SerializeString(value.ToString());
                }
            }
        }
    }

}
