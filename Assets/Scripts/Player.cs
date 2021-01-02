using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
	public int id;
	public GameObject player;
	public Player(int _id, GameObject _player)
	{
		id = _id;
		player = _player;
	}
}
