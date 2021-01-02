using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    private Vector3 firstPos;
    void Start()
    {
        firstPos = gameObject.transform.position;
    }

    public void StartAnimation(string text,Color32 color)
	{
        StopAllCoroutines();
        gameObject.transform.position = firstPos;
        GetComponent<TextMeshProUGUI>().text = text;
        GetComponent<TextMeshProUGUI>().color = color;
        StartCoroutine(_StartAnimation());
    }

    IEnumerator _StartAnimation()
	{
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        LeanTween.moveLocalY(gameObject, point.y, 0.50f);
        yield return new WaitForSeconds(4f);
        LeanTween.moveLocalY(gameObject, firstPos.y, 0.50f);
    }
}
