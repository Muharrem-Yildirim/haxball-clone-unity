using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.VFX;

public class Manager : MonoBehaviour
{
    public delegate int _OnScore(Teams team);
    public event _OnScore OnScore;

    public TextMeshProUGUI matchTimeText;
    public TextMeshProUGUI matchScoreText;

    public static Manager instance;

    public int[] scores = new int[2];
    private SpriteRenderer _rink;
    public SpriteRenderer rink
    {
        get
        {
            if(_rink == null)
			{
                _rink = GameObject.Find("Rink").GetComponent<SpriteRenderer>();
                return _rink;
			}
            return _rink;
		}
		set
		{
            _rink = value;
		}
    }
    public bool waitingForRefresh;
    public float matchTime;

    [SerializeField]
    public List<Team> teams = new List<Team>();

    public enum Teams
    {
        Red,
        Blue
    };

    //void Start()
    //{
        
    //    OnScore += OnScore;
    //    //SceneManager.sceneLoaded += MatchNetwork.OnSceneLoaded;
    //}

	public void Start()
	{
        if (instance == null)
            instance = this;

        matchScoreText = GameObject.Find("Top Bar").transform.GetChild(0).transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        matchTimeText = GameObject.Find("Top Bar").transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        LayoutRebuilder.ForceRebuildLayoutImmediate(GameObject.Find("Top Bar").transform.GetChild(0).transform.GetComponent<RectTransform>());
        LayoutRebuilder.ForceRebuildLayoutImmediate(GameObject.Find("Top Bar").transform.GetChild(1).transform.GetComponent<RectTransform>());

        ToggleTopBar(false);
    }

    public void ToggleTopBar(bool state) {
        GameObject.Find("Top Bar").GetComponent<CanvasGroup>().alpha = state == true ? 1 : 0;
        GameObject.Find("Top Bar").GetComponent<CanvasGroup>().blocksRaycasts = state;
    }

    public void SpawnBall()
	{
        matchTime = Time.time;
        GameObject prefab = GameObject.Instantiate(Resources.Load("Prefabs/Ball"), rink.transform.position,Quaternion.identity) as GameObject;
        prefab.name = "Ball";
        prefab.tag = "Ball";
        NetworkServer.Spawn(prefab);
    }

    private void OnGoal(string _team)
	{
        MatchServer.instance.updateBorderMask(_team == "Red" ? "Blue" : "Red");
        MatchServer.instance.OnGoal(_team);
        StartCoroutine(teleportBall());
    }

    public string getNextTeamSlot()
	{
        if (Manager.instance.teams.Find(_team => _team.name == "Red").players.Count == 0 && Manager.instance.teams.Find(_team => _team.name == "Blue").players.Count == 0)
            return "Red";
        else if (Manager.instance.teams.Find(_team => _team.name == "Red").players.Count == Manager.instance.teams.Find(_team => _team.name == "Blue").players.Count)
            return "Red";
        else if (Manager.instance.teams.Find(_team => _team.name == "Red").players.Count > Manager.instance.teams.Find(_team => _team.name == "Blue").players.Count)
            return "Blue";
        else if (Manager.instance.teams.Find(_team => _team.name == "Red").players.Count < Manager.instance.teams.Find(_team => _team.name == "Blue").players.Count)
            return "Red";
        else
            return "Red";
    }

    private IEnumerator teleportBall()
    {
        yield return new WaitForSeconds(3f);
        if (NetworkServer.active)
        {
            GameObject ball = GameObject.FindWithTag("Ball");
            ball.GetComponent<Ball>().teleportBall(Vector2.zero + (Vector2)rink.transform.position,Vector2.zero);
            waitingForRefresh = false;

            foreach (Team team in Manager.instance.teams)
            {
                foreach (Player player in Manager.instance.GetTeam(team.name).players)
                {
                    Transform spawns = GameObject.FindWithTag(team.name + " Spawns").transform;
                    Transform startPos = spawns.GetChild(UnityEngine.Random.Range(0, spawns.childCount));

                    player.player.GetComponent<PlayerController>().teleportPlayer(startPos.position);
                }
            }

        }
    }

    public Team GetTeam(string team)
    {
        return Manager.instance.teams.Find(_team => _team.name == team);
    }

    public void SetScore(string _team)
    {
        if (waitingForRefresh)
            return;
        waitingForRefresh = true;
        Team team = GetTeam(_team);
        team.score = team.score + 1;
        MatchServer.instance.updateScore(_team, team.score);
        OnGoal(_team);
    }

    public void SetScore(string _team,int _score)
    {
        Team team = GetTeam(_team);
        team.score = _score;
        MatchServer.instance.updateScore(_team, team.score);
    }

    void Update()
    {
        if (!NetworkClient.active)
            return;
        if (matchTimeText == null)
            return;
        TimeSpan time = TimeSpan.FromSeconds(matchTime);
        matchTimeText.text = time.ToString(@"mm\:ss");
    }
}
