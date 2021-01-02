using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KickInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	public bool pointerdown;
	public PlayerController localplayer;
	void Start()
	{
		/*transform.GetChild(0).transform.GetComponent<Button>().onClick.AddListener(() => Kick());*/
		//localplayer = GameObject.FindWithTag("Local Player").GetComponent<PlayerController>();
	}

	void Kick()
	{
		GameObject.FindWithTag("Local Player").GetComponent<PlayerController>().Kick();
	}

	// Update is called once per frame
	void Update()
	{
		if (localplayer == null)
		{
			transform.GetChild(0).transform.GetComponent<CanvasGroup>().alpha = 0;
			transform.GetChild(0).transform.GetComponent<CanvasGroup>().blocksRaycasts = false;
			return;
		}
		if (pointerdown)
		{
			transform.GetChild(0).transform.GetComponent<CanvasGroup>().alpha = 1;
			transform.GetChild(0).transform.GetComponent<CanvasGroup>().blocksRaycasts = true;
			Kick();
		}
		else
		{
			if (!Input.GetKey(KeyCode.Space))
			{
				if (localplayer.alreadySentPressingState == false)
				{
					localplayer.CmdIsPressingSpace(false);
					localplayer.alreadySentPressingState = true;
				}
			}
			transform.GetChild(0).transform.GetComponent<CanvasGroup>().alpha = 0;
			transform.GetChild(0).transform.GetComponent<CanvasGroup>().blocksRaycasts = false;
		}
		if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
		{
			if (Input.GetMouseButton(0))
			{
				//transform.GetChild(0).transform.GetComponent<Button>().onClick.Invoke();
				transform.GetChild(0).transform.position = Input.mousePosition;
			}
		}
		else
		{
			if (Input.touchCount > 0)
			{
				Touch touch = Input.GetTouch(0);
				transform.GetChild(0).transform.position = touch.position;
			}
		}

	}

	public void OnPointerDown(PointerEventData eventData)
	{
		//if(EventSystem. == gameObject)
		pointerdown = true;
		transform.GetChild(0).transform.position = eventData.position;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		pointerdown = false;
	}


}
