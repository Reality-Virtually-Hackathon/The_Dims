using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.Linq;
using System.IO;
using UnityEngine.Video;

public class GameLogic : MonoBehaviour
{

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
    void Start()
    {

        Instance = this;

        SetupTeamMetadata();

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
        listRow3.GetComponent<Renderer>().sharedMaterial = selectionMaterials[0];

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

        mainScreenPlayer.clip = superBowlClips[rowNum];
    }

    // Update is called once per frame
    void Update()
    {
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
}
