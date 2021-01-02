using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class MatchServer : NetworkBehaviour
{
    public static MatchServer instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    [ClientRpc]
    public void updateScore(string _team, int _score)
    {
        Manager.instance.GetTeam(_team).score = _score;
    }

    [ClientRpc]
    public void updateMatchTime(float _time)
    {
        Manager.instance.matchTime = _time;
    }

    [ClientRpc]
    public void OnGoal(string _team)
	{
        if (_team == "Red")
            GameObject.Find("Goal Text").GetComponent<ScoreText>().StartAnimation("RED TEAM\n SCORED", new Color32(215, 22, 66, 255));
        else
            GameObject.Find("Goal Text").GetComponent<ScoreText>().StartAnimation("BLUE TEAM\n SCORED", new Color32(120, 255, 255, 255));
        Manager.instance.matchScoreText.text = Manager.instance.GetTeam("Red").score + " - " + +Manager.instance.GetTeam("Blue").score;
    }

    [ClientRpc]
    public void resetBorderMask()
    {
        GameObject.Find("Center Circle Left").layer = 0;
        GameObject.Find("Center Circle Right").layer = 0;

        GameObject.Find("7").layer = 0;
        GameObject.Find("1").layer = 0;
        GameObject.Find("10").layer = 0;

        GameObject.Find("Center Circle Left").GetComponent<EdgeCollider2D>().isTrigger = true;
        GameObject.Find("Center Circle Right").GetComponent<EdgeCollider2D>().isTrigger = true;

        GameObject.Find("7").GetComponent<BoxCollider2D>().isTrigger = true;
        GameObject.Find("1").GetComponent<BoxCollider2D>().isTrigger = true;
        GameObject.Find("10").GetComponent<BoxCollider2D>().isTrigger = true;
        GameObject.Find("11").GetComponent<BoxCollider2D>().isTrigger = true;
    }

    [ClientRpc]
    public void updateBorderMask(string _team)
    {
        if(_team == "Red")
		{
            GameObject.Find("Center Circle Left").layer = 0;
            GameObject.Find("Center Circle Right").layer = LayerMask.NameToLayer("Center Collision");

            GameObject.Find("7").layer = LayerMask.NameToLayer("Center Collision");
            GameObject.Find("1").layer = LayerMask.NameToLayer("Center Collision");
            GameObject.Find("10").layer = LayerMask.NameToLayer("Center Collision");

            GameObject.Find("Center Circle Left").GetComponent<EdgeCollider2D>().isTrigger = true;
            GameObject.Find("Center Circle Right").GetComponent<EdgeCollider2D>().isTrigger = false;

            GameObject.Find("7").GetComponent<BoxCollider2D>().isTrigger = false;
            GameObject.Find("1").GetComponent<BoxCollider2D>().isTrigger = false;
            GameObject.Find("10").GetComponent<BoxCollider2D>().isTrigger = false;
            GameObject.Find("11").GetComponent<BoxCollider2D>().isTrigger = false;
        }
        else
		{
            GameObject.Find("Center Circle Left").layer = LayerMask.NameToLayer("Center Collision");
            GameObject.Find("Center Circle Right").layer = 0;

            GameObject.Find("7").layer = LayerMask.NameToLayer("Center Collision");
            GameObject.Find("1").layer = LayerMask.NameToLayer("Center Collision");
            GameObject.Find("10").layer = LayerMask.NameToLayer("Center Collision");

            GameObject.Find("Center Circle Left").GetComponent<EdgeCollider2D>().isTrigger = false;
            GameObject.Find("Center Circle Right").GetComponent<EdgeCollider2D>().isTrigger = true;

            GameObject.Find("7").GetComponent<BoxCollider2D>().isTrigger = false;
            GameObject.Find("1").GetComponent<BoxCollider2D>().isTrigger = false;
            GameObject.Find("10").GetComponent<BoxCollider2D>().isTrigger = false;
            GameObject.Find("11").GetComponent<BoxCollider2D>().isTrigger = false;
        }

    }
}
