using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Team 
{
	public string name = "";
	public int score = 0;
	public List<Player> players = new List<Player>();
}
