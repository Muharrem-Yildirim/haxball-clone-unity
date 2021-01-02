using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

//red team player color = #E0655C
//blue team player color = #578FDF
public class PlayerController : NetworkBehaviour
{
	private Rigidbody2D rigidbody;
	private NetworkIdentity networkIdentity;
	private FloatingJoystick floatingJoystick;

	[SyncVar]
	public int playerNumber;

	public string playerTeam;

	[SyncVar(hook = nameof(updatePlayerColor))]
	public Color playerColor;
	//public Vector2 force;

	[SyncVar(hook = nameof(updateVelocity))]
	public Vector2 velocity;

	private Vector2 networkPos = Vector2.zero;
	private Vector2 withoutNetworkPos = Vector2.zero;


	private void Awake()
	{
		networkIdentity = GetComponent<NetworkIdentity>();
	}
	void Start()
	{
		rigidbody = GetComponent<Rigidbody2D>();
		networkPos = transform.position;
		if (networkIdentity.isLocalPlayer)
		{
			transform.tag = "Local Player";
			floatingJoystick = GameObject.Find("Floating Joystick").GetComponent<FloatingJoystick>();
			GameObject.Find("Kick Joystick").GetComponent<KickInput>().localplayer = this;
		}
		playerNumber = (int)networkIdentity.netId;
		setPlayerNumber(playerNumber);
		transform.GetChild(0).transform.GetComponent<SpriteRenderer>().color = playerColor;
		transform.name = "Player " + playerNumber;
	}

	//[ClientRpc]
	public void setPlayerTeam(string _team)
	{
		Color newCol;
		if (ColorUtility.TryParseHtmlString(_team == "Red" ? "#E0655C" : "#578FDF", out newCol))
			playerColor = newCol;
		playerTeam = _team;
	}

	[ClientRpc]
	public void teleportPlayer(Vector2 _position)
	{
		networkPos = _position;
		rigidbody.position = _position;
		rigidbody.velocity = Vector2.zero;
	}


	void updatePlayerColor(Color oldValue, Color newValue)
	{
		transform.GetChild(0).transform.GetComponent<SpriteRenderer>().color = newValue;
	}

	void updateVelocity(Vector2 oldValue, Vector2 newValue)
	{
		rigidbody.velocity = newValue;
	}

	void setPlayerNumber(int number)
	{
		transform.GetChild(0).transform.GetChild(0).transform.GetComponent<TextMeshPro>().text = number.ToString();
	}

	void Update()
	{
		if (!networkIdentity.isLocalPlayer)
		{
			//if(velocity != rigibody.velocity)
			//rigidbody.velocity = velocity;
			return;
		}
		float inputY = 0;//Input.GetAxis("Vertical");
		float inputX = 0;//Input.GetAxis("Horizontal");

		//force += new Vector2(inputY, inputX) * 0.2f;
		//force = new Vector2(Mathf.Clamp(force.x,-5f,5f), Mathf.Clamp(force.y, -5f, 5f));

		//if (inputX == 0 && inputY == 0)
		//{
		//	force -= new Vector2(0, 0);
		//}

		if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
		{
			inputY = Input.GetAxis("Vertical");
			inputX = Input.GetAxis("Horizontal");
			if (Input.GetKey(KeyCode.Space))
			{
				Kick();
			}
			else
			{
				if (alreadySentPressingState == false)
				{
					CmdIsPressingSpace(false);
					alreadySentPressingState = true;
				}
			}
		}
		else
		{
			inputY = floatingJoystick.Vertical;//Input.GetAxis("Vertical");
			inputX = floatingJoystick.Horizontal;//Input.GetAxis("Horizontal");
		}

		sendVelocity(new Vector2(inputX, inputY) * 5, transform.position, NetworkTime.time);
		// updatePosition(transform.position, NetworkTime.time);
		rigidbody.velocity = new Vector2(inputX, inputY) * 5;
	}

	public void Kick()
	{
		CmdKick();
		CmdIsPressingSpace(true);
		alreadySentPressingState = false;
	}

	[Command]
	public void CmdKick()
	{
		Rigidbody2D rigidbody = GameObject.FindWithTag("Ball").GetComponent<Rigidbody2D>();
		Vector2 direction = rigidbody.transform.position - transform.position;
		//Debug.Log("Ball Kick: " + Vector2.Distance(rigidbody.position, transform.position));
		if (Vector2.Distance(rigidbody.position, transform.position) <= 1.20f)
			rigidbody.AddForce(rigidbody.mass * 200 * direction);
	}

	public bool alreadySentPressingState = false;
	[Command]
	public void CmdIsPressingSpace(bool state)
	{
		isPressingSpace(state);
	}

	[ClientRpc]
	void isPressingSpace(bool state)
	{
		if (state)
			GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0.9f);
		else
			GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.9f);
	}

	[Command]
	void sendVelocity(Vector2 _velocity, Vector2 _position, double lastTime)
	{

		//if (_velocity == Vector2.zero || _position == (Vector2)transform.position)
		//	return;
		float lag = Mathf.Abs((float)(NetworkTime.time - lastTime));
		//Debug.Log("sendVelocity " + transform.name + " - lag: " + lag + " velocity: " + _velocity + " networkTime: " + NetworkTime.time + " lastTime: " + lastTime);

		//rigidbody.position = _position;
		//rigidbody.velocity = _velocity;

		//rigidbody.position = Vector3.Lerp(rigidbody.position, rigidbody.velocity * lag, 0.1f);
		withoutNetworkPos = _position;

		sendVelocityToPlayer(_velocity, _position, lastTime);
	}

	[ClientRpc]
	void sendVelocityToPlayer(Vector2 _velocity, Vector2 _position, double lastTime)
	{

		if (rigidbody == null)
			return;
		//if (_velocity == Vector2.zero)
		//    return;
		float lag = Mathf.Abs((float)(NetworkTime.time - lastTime));
		//Debug.Log("sendVelocityToPlayer " + transform.name + " - lag: " + lag + " velocity: " + _velocity + " networkTime: " + NetworkTime.time + " lastTime: " + lastTime);

		networkPos = _position;

		if (!isLocalPlayer)
			rigidbody.velocity = _velocity;

		networkPos += rigidbody.velocity * lag;
	}

	public void FixedUpdate()
	{
		if (!isLocalPlayer)
		{
			rigidbody.position = Vector3.Lerp(rigidbody.position, networkPos, Time.fixedDeltaTime);
			//rigidbody.rotation = Quaternion.RotateTowards(rigidbody.rotation, pos, Time.fixedDeltaTime * 100.0f); s
		}
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = new Color(1, 1, 1, 0.5f);
		Gizmos.DrawSphere(withoutNetworkPos, 0.5f);
	}


	//[ClientRpc]
	//void updatePosition(Vector3 serverPos, double lastTime)
	//{
	//	if (Vector2.Distance(transform.position, serverPos) > 0.01f)
	//	{
	//		Debug.Log("pozisyon sunucuyla eşleşmedi");
	//		transform.position = serverPos;
	//	}
	//}
	//private void OnCollisionEnter2D(Collision2D collision)
	//{
	//	Debug.Log(collision.transform.name);
	//}
}
