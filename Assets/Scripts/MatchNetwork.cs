using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchNetwork : NetworkManager
{
	 public GameObject asdasa;

	public override void Start()
	{
		base.Start();
		SceneManager.sceneLoaded += OnSceneLoaded;
	}
	public override void OnStartServer()
	{
		base.OnStartServer();
		//ServerChangeScene("Rink");
		CancelInvoke();
		Invoke("incrementMatchTime", 1f);

		Manager.instance.ToggleTopBar(true);
	}

	public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (scene.name == "Rink")
		{
			if (NetworkServer.active)
			{
				GameObject Server = GameObject.Instantiate(Resources.Load("Prefabs/Server") as GameObject);
				NetworkServer.Spawn(Server);

				Manager.instance.SpawnBall();
				MatchServer.instance.resetBorderMask();

				Manager.instance.matchTimeText.text = "00:00";
				Manager.instance.matchScoreText.text = "0 - 0";

				Manager.instance.matchTime = 0;
			}

			Manager.instance.Start();
			Manager.instance.ToggleTopBar(true);
		}
	}

	void incrementMatchTime()
	{
		Manager.instance.matchTime++;
		if (NetworkServer.active)
		{
			Invoke("incrementMatchTime", 1f);
			MatchServer.instance.updateMatchTime(Manager.instance.matchTime);
		}
	}

	public override void OnStartClient()
	{
		base.OnStartClient();
		Manager.instance.GetTeam("Red").score = 0;
		Manager.instance.GetTeam("Blue").score = 0;
	}

	public override void OnStopClient()
	{
		base.OnStopClient();
		asdasd();
	}

	public override void OnStopHost()
	{
		base.OnStopHost();
		asdasd();
	}

	public void asdasd()
	{
		Manager.instance.GetTeam("Red").score = 0;
		Manager.instance.GetTeam("Blue").score = 0;

		Manager.instance.GetTeam("Red").players = new List<Player>();
		Manager.instance.GetTeam("Blue").players = new List<Player>();

		CancelInvoke();

		Manager.instance.matchTime = 0;
		Manager.instance.matchTimeText.text = "00:00";
		Manager.instance.matchScoreText.text = "0 - 0";

		Manager.instance.ToggleTopBar(false);
	}

	public override void OnServerAddPlayer(NetworkConnection conn)
	{
		string team = Manager.instance.getNextTeamSlot();

		Transform spawns = GameObject.FindWithTag(team + " Spawns").transform;
		Transform startPos = spawns.GetChild(Random.Range(0, spawns.childCount));

		GameObject player = startPos != null
			? Instantiate(playerPrefab, startPos.position, startPos.rotation)
			: Instantiate(playerPrefab);
		NetworkServer.AddPlayerForConnection(conn, player);

		addPlayerToTeam(conn, player, team);
	}
	
	public override void OnServerDisconnect(NetworkConnection conn)
	{
		base.OnServerDisconnect(conn);
		Debug.Log("server disconnect "+conn.connectionId);
		Manager.instance.GetTeam("Red").players.Remove(Manager.instance.GetTeam("Red").players.Where(plr => plr.id == conn.connectionId).FirstOrDefault());
		Manager.instance.GetTeam("Blue").players.Remove(Manager.instance.GetTeam("Blue").players.Where(plr => plr.id == conn.connectionId).FirstOrDefault());
	}



	void addPlayerToTeam(NetworkConnection conn,GameObject player,string team)
	{
		Manager.instance.teams.Find(_team => _team.name == team).players.Add(new Player(conn.connectionId,player));
		player.GetComponent<PlayerController>().setPlayerTeam(team);
	}
}
