using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : NetworkBehaviour
{
    private Rigidbody2D rigidbody;

	private void Awake()
	{
		transform.name = "Ball";
	}

	void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
		networkPos = transform.position;
	}

    void Update()
    {
		if(NetworkServer.active)
		sendVelocityToPlayer(rigidbody.velocity, transform.position, NetworkTime.time);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (NetworkServer.active && collision.transform.tag.Contains("Player") && !Manager.instance.waitingForRefresh)
			MatchServer.instance.resetBorderMask();
	}



	Vector2 networkPos = Vector2.zero;
	[ClientRpc]
	void sendVelocityToPlayer(Vector2 _velocity, Vector2 _position, double lastTime)
	{
		if (rigidbody == null)
			return;
		float lag = Mathf.Abs((float)(NetworkTime.time - lastTime));
		//Debug.Log("sendVelocityToPlayer " + transform.name + " - lag: " + lag + " velocity: " + _velocity + " networkTime: " + NetworkTime.time + " lastTime: " + lastTime);

		networkPos = _position;

		rigidbody.velocity = _velocity;

		networkPos += rigidbody.velocity * lag;
	}

	[ClientRpc]
	public void teleportBall(Vector2 _position, Vector2 _velocity)
	{
		if (rigidbody == null)
			return;
		networkPos = _position;
		transform.position = _position;
		rigidbody.velocity = Vector2.zero;
	}


	public void FixedUpdate()
	{
		if (rigidbody == null)
			return;
		if (!isLocalPlayer)
		{
			rigidbody.position = Vector3.Lerp(rigidbody.position, networkPos, Time.fixedDeltaTime);
			//rigidbody.rotation = Quaternion.RotateTowards(rigidbody.rotation, pos, Time.fixedDeltaTime * 100.0f);
		}
	}
}
