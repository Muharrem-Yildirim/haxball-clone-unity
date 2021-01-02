using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public string towerTeam;
    public SpriteRenderer[] borders = new SpriteRenderer[3];
    private bool effect;
    private float lastEffectTime;
    private int effectCallTimes;

	private void Start()
	{
        borders[0] = GetComponent<SpriteRenderer>();

    }

	public void SetTeam(string _team)
    {
        towerTeam = _team;
    }

    public string GetTeam()
    {
        return towerTeam;
    }

	private void Update()
	{
        float curTime = Time.time;
        if (effect && curTime-lastEffectTime > 0.30f)
        {
			if (borders[0] != null)
			{
				if ((int)(borders[0].color.a * 255f) == 255)
				{
					for (int i = 0; i <= 2; i++)
						if (borders[i] != null)
							borders[i].color = new Color(202f / 255f, 230f / 255f, 192 / 255f, 75f / 255f);
				}
				else
				{
					for (int i = 0; i <= 2; i++)
						if (borders[i] != null)
							borders[i].color = new Color(202f / 255f, 230f / 255f, 192 / 255f, 1f);
				}
			}

			if (effectCallTimes > 4)
			{
                effect = false;
                effectCallTimes = 0;
                for (int i = 0; i <= 2; i++)
                    if (borders[i] != null)
                        borders[i].color = new Color(202f / 255f, 230f / 255f, 192 / 255f, 1f);
            }

            lastEffectTime = Time.time;
            effectCallTimes++;
        }
	}

    

    private void OnCollisionEnter2D(Collision2D collision)
	{
        if (collision.transform.CompareTag("Ball"))
        {
            effect = true;
            effectCallTimes = 0;

            if (!NetworkServer.active)
                return;
            Manager.instance.SetScore(towerTeam == "Blue" ? "Red" : "Blue");
        }
    }
}
