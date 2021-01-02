using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Borders : MonoBehaviour
{
    public GameObject[] borderLines;
    public SpriteRenderer rink;
    private void Start()
    {
        UpdateBorders();
    }

	//private void Update()
	//{
	//	UpdateBorders();
	//}

	void UpdateBorders()
	{
        //center
        //borderLines[0].transform.localScale = new Vector2(0.25f, Vector2.Distance(borderLines[1].transform.position,GameObject.Find("Center Circle").transform.position)*1);
        ////borderLines[0].GetComponent<SpriteRenderer>().size.Set(10f,5f);
        ////borderLines[0].transform.position = new Vector2(rink.transform.position.x, rink.transform.position.y);
        //borderLines[0].name = "1";

		//bottom
		borderLines[1].transform.localScale = new Vector2(rink.transform.localScale.x, 0.25f);
		borderLines[1].transform.position = new Vector2(rink.transform.position.x, rink.transform.position.y - ((rink.GetComponent<SpriteRenderer>().bounds.size.y / 2)));
        borderLines[1].name = "2";

        //center top
        borderLines[0].transform.localScale = new Vector2(0.25f, rink.transform.localScale.y / 3 + 0.25f);
        borderLines[0].transform.position = new Vector2(rink.transform.position.x, rink.transform.position.y + (borderLines[0].GetComponent<SpriteRenderer>().bounds.size.y - borderLines[1].GetComponent<SpriteRenderer>().bounds.size.y));
        borderLines[0].name = "1";
        //borderLines[0].layer = 10;

        //center bottom
        borderLines[7].transform.localScale = new Vector2(0.25f, rink.transform.localScale.y / 3 + 0.25f);
        borderLines[7].transform.position = new Vector2(rink.transform.position.x, rink.transform.position.y - (borderLines[0].GetComponent<SpriteRenderer>().bounds.size.y - borderLines[1].GetComponent<SpriteRenderer>().bounds.size.y));
        borderLines[7].name = "7";
        //borderLines[7].layer = LayerMask.NameToLayer("Center Collision");

        //center
        borderLines[8].transform.localScale = new Vector2(0.25f, rink.transform.localScale.y / 3 + 0.25f);
        borderLines[8].transform.position = new Vector2(rink.transform.position.x, rink.transform.position.y);
        borderLines[8].name = "9";
        //borderLines[8].layer = LayerMask.NameToLayer("Center Collision");
        Destroy(borderLines[8].GetComponent<BoxCollider2D>());

        //center full top
        borderLines[9].transform.localScale = new Vector2(0.25f, 8f);
        borderLines[9].transform.position = new Vector2(rink.transform.position.x, rink.transform.position.y+(rink.bounds.size.y/2));
        borderLines[9].name = "10";
        borderLines[9].layer = LayerMask.NameToLayer("Center Collision");
        borderLines[9].GetComponent<BoxCollider2D>().isTrigger = true;
        borderLines[9].GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);

        //center full bottom
        borderLines[10].transform.localScale = new Vector2(0.25f, 8f);
        borderLines[10].transform.position = new Vector2(rink.transform.position.x, rink.transform.position.y + (rink.bounds.size.y / -2));
        borderLines[10].name = "11";
        borderLines[10].layer = LayerMask.NameToLayer("Center Collision");
        borderLines[10].GetComponent<BoxCollider2D>().isTrigger = true;
        borderLines[10].GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);

        //top
        borderLines[2].transform.localScale = new Vector2(rink.transform.localScale.x, 0.25f);
        borderLines[2].transform.position = new Vector2(rink.transform.position.x, rink.transform.position.y - (rink.GetComponent<SpriteRenderer>().bounds.size.y / -2));
        borderLines[2].name = "3";
        

        ////right
        //borderLines[3].transform.localScale = new Vector2(0.25f, rink.transform.localScale.y + 0.25f);
        //borderLines[3].transform.position = new Vector2(rink.transform.position.x - (rink.GetComponent<SpriteRenderer>().bounds.size.x / -2), rink.transform.position.y);
        //borderLines[3].name = "4";

        //right bottom
        borderLines[3].transform.localScale = new Vector2(0.25f, rink.transform.localScale.y / 3 + 0.25f);
        borderLines[3].transform.position = new Vector2(rink.GetComponent<SpriteRenderer>().bounds.size.x + rink.transform.position.x - (rink.GetComponent<SpriteRenderer>().bounds.size.x / 2), rink.transform.position.y - (borderLines[3].GetComponent<SpriteRenderer>().bounds.size.y - borderLines[1].GetComponent<SpriteRenderer>().bounds.size.y));
        borderLines[3].name = "4";

        //right top
        borderLines[4].transform.localScale = new Vector2(0.25f, rink.transform.localScale.y / 3 + 0.25f);
        borderLines[4].transform.position = new Vector2(rink.GetComponent<SpriteRenderer>().bounds.size.x + rink.transform.position.x - (rink.GetComponent<SpriteRenderer>().bounds.size.x / 2), rink.transform.position.y + (borderLines[3].GetComponent<SpriteRenderer>().bounds.size.y - borderLines[1].GetComponent<SpriteRenderer>().bounds.size.y));
        borderLines[4].name = "5";

        //left bottom
        borderLines[5].transform.localScale = new Vector2(0.25f, rink.transform.localScale.y / 3 + 0.25f);
        borderLines[5].transform.position = new Vector2(rink.transform.position.x - (rink.GetComponent<SpriteRenderer>().bounds.size.x / 2), rink.transform.position.y - (borderLines[4].GetComponent<SpriteRenderer>().bounds.size.y - borderLines[1].GetComponent<SpriteRenderer>().bounds.size.y));
        borderLines[5].name = "6";

        //left top
        borderLines[6].transform.localScale = new Vector2(0.25f, rink.transform.localScale.y / 3 + 0.25f);
        borderLines[6].transform.position = new Vector2(rink.transform.position.x - (rink.GetComponent<SpriteRenderer>().bounds.size.x / 2), rink.transform.position.y + (borderLines[5].GetComponent<SpriteRenderer>().bounds.size.y - borderLines[1].GetComponent<SpriteRenderer>().bounds.size.y));
        borderLines[6].name = "8";

    }

    //private void UpdateBorders()
    //   {

    //       //center
    //       borderLines[0].transform.localScale = new Vector2(0.004f, (Vector2.Distance(GameObject.Find("Center Circle").transform.position, borderLines[3].transform.position) - borderLines[3].GetComponent<SpriteRenderer>().bounds.size.y / 2) / 10);
    //       borderLines[0].transform.position = new Vector2(rink.transform.position.x, rink.transform.position.y);
    //       borderLines[0].transform.name = "Center";
    //       //borderLines[0].transform.GetComponent<SpriteRenderer>().color = new Color(1,1,1,120f/255f);
    //       Destroy(borderLines[0].transform.GetComponent<Collider2D>());

    //       //right top
    //       borderLines[1].transform.localScale = new Vector2(0.004f, 1 / 3f);
    //       borderLines[1].transform.position = new Vector2(rink.transform.position.x + ((rink.bounds.size.x / 2f) + (borderLines[0].GetComponent<SpriteRenderer>().bounds.size.x / 2)), rink.transform.position.y + (borderLines[1].GetComponent<SpriteRenderer>().bounds.size.y));
    //       borderLines[1].transform.name = "Right Top";

    //       //right bottom
    //       borderLines[9].transform.localScale = new Vector2(0.004f, 1 / 3f);
    //       borderLines[9].transform.position = new Vector2(rink.transform.position.x + ((rink.bounds.size.x / 2f) + (borderLines[0].GetComponent<SpriteRenderer>().bounds.size.x / 2)), rink.transform.position.y + (borderLines[1].GetComponent<SpriteRenderer>().bounds.size.y * -1));
    //       borderLines[9].transform.name = "Right Bottom";

    //       //right tower top
    //       borderLines[10].transform.localScale = new Vector2(1f /  24f, 0.008f);
    //       borderLines[10].transform.position = new Vector2(rink.transform.position.x + rink.bounds.size.x  + (-(rink.bounds.size.x / 2) + (borderLines[10].GetComponent<SpriteRenderer>().bounds.size.x / 2)), rink.transform.position.y + borderLines[10].GetComponent<SpriteRenderer>().bounds.size.y / 2 + (borderLines[2].GetComponent<SpriteRenderer>().bounds.size.y + (borderLines[1].GetComponent<SpriteRenderer>().bounds.size.y / -2)));
    //       borderLines[10].transform.name = "Right Top Tower";

    //       //right tower
    //       borderLines[12].transform.localScale = new Vector2(0.004f, 1 / 3f);
    //       borderLines[12].transform.position = new Vector2(rink.transform.position.x + borderLines[10].GetComponent<SpriteRenderer>().bounds.size.x  + ((rink.bounds.size.x / 2f) + (borderLines[0].GetComponent<SpriteRenderer>().bounds.size.x / 2)), rink.transform.position.y);
    //       borderLines[12].transform.name = "Right Tower";
    //       borderLines[12].AddComponent<Tower>().SetTeam("Blue");


    //       //right tower bottom
    //       borderLines[11].transform.localScale = new Vector2(1f / 24f, 0.008f);
    //       borderLines[11].transform.position = new Vector2(rink.transform.position.x + rink.bounds.size.x + (-(rink.bounds.size.x / 2) + (borderLines[11].GetComponent<SpriteRenderer>().bounds.size.x / 2)), rink.transform.position.y - borderLines[11].GetComponent<SpriteRenderer>().bounds.size.y / 2 + (borderLines[2].GetComponent<SpriteRenderer>().bounds.size.y * -1 + (borderLines[1].GetComponent<SpriteRenderer>().bounds.size.y / 2)));
    //       borderLines[11].transform.name = "Right Bottom Tower";
    //       borderLines[12].GetComponent<Tower>().borders[0] = borderLines[11].GetComponent<SpriteRenderer>();
    //       borderLines[12].GetComponent<Tower>().borders[1] = borderLines[12].GetComponent<SpriteRenderer>();
    //       borderLines[12].GetComponent<Tower>().borders[2] = borderLines[10].GetComponent<SpriteRenderer>();

    //       //left top
    //       borderLines[2].transform.localScale = new Vector2(0.004f, 1 / 3f);
    //       borderLines[2].transform.position = new Vector2(rink.transform.position.x + (-(rink.bounds.size.x / 2) - (borderLines[0].GetComponent<SpriteRenderer>().bounds.size.x / 2)), rink.transform.position.y + (borderLines[2].GetComponent<SpriteRenderer>().bounds.size.y));
    //       borderLines[2].transform.name = "Left Top";

    //       //left bottom
    //       borderLines[5].transform.localScale = new Vector2(0.004f, 1 / 3f);
    //       borderLines[5].transform.position = new Vector2(rink.transform.position.x + (-(rink.bounds.size.x / 2) - (borderLines[0].GetComponent<SpriteRenderer>().bounds.size.x / 2)), rink.transform.position.y + (borderLines[2].GetComponent<SpriteRenderer>().bounds.size.y * -1));
    //       borderLines[5].transform.name = "Left Bottom";

    //       //left tower top
    //       borderLines[7].transform.localScale = new Vector2(1f / 24f, 0.008f);
    //       borderLines[7].transform.position = new Vector2(rink.transform.position.x + (-(rink.bounds.size.x / 2) - (borderLines[7].GetComponent<SpriteRenderer>().bounds.size.x / 2)), rink.transform.position.y - borderLines[7].GetComponent<SpriteRenderer>().bounds.size.y / 2 + (borderLines[2].GetComponent<SpriteRenderer>().bounds.size.y + (borderLines[2].GetComponent<SpriteRenderer>().bounds.size.y / -2)));
    //       borderLines[7].transform.name = "Left Top Tower";


    //       //left tower
    //       borderLines[13].transform.localScale = new Vector2(0.004f, 1 / 3f);
    //       borderLines[13].transform.position = new Vector2(rink.transform.position.x + (-borderLines[7].GetComponent<SpriteRenderer>().bounds.size.x) + (-(rink.bounds.size.x / 2) - (borderLines[0].GetComponent<SpriteRenderer>().bounds.size.x / 2)), rink.transform.position.y );
    //       borderLines[13].transform.name = "Left Tower";
    //       borderLines[13].AddComponent<Tower>().SetTeam("Red");

    //       //left tower bottom
    //       borderLines[8].transform.localScale = new Vector2(1f / 24f, 0.008f);
    //       borderLines[8].transform.position = new Vector2(rink.transform.position.x + (-(rink.bounds.size.x / 2) - (borderLines[8].GetComponent<SpriteRenderer>().bounds.size.x / 2)), rink.transform.position.y + borderLines[8].GetComponent<SpriteRenderer>().bounds.size.y / 2  + (borderLines[2].GetComponent<SpriteRenderer>().bounds.size.y * -1 + (borderLines[2].GetComponent<SpriteRenderer>().bounds.size.y/2)));
    //       borderLines[8].transform.name = "Left Bottom Tower";
    //       borderLines[13].GetComponent<Tower>().borders[0] = borderLines[8].GetComponent<SpriteRenderer>();
    //       borderLines[13].GetComponent<Tower>().borders[1] = borderLines[13].GetComponent<SpriteRenderer>();
    //       borderLines[13].GetComponent<Tower>().borders[2] = borderLines[7].GetComponent<SpriteRenderer>();

    //       //top
    //       borderLines[3].transform.position = new Vector2(rink.transform.position.x, rink.transform.position.y + ((rink.bounds.size.y / 2f) + (borderLines[0].GetComponent<SpriteRenderer>().bounds.size.x / 2)));
    //       borderLines[3].transform.localScale = new Vector2(1+(0.004f*2), 0.008f);
    //       borderLines[3].transform.name = "Top";

    //       //bottom
    //       borderLines[4].transform.position = new Vector2(rink.transform.position.x, rink.transform.position.y + (-(rink.bounds.size.y / 2f) - (borderLines[0].GetComponent<SpriteRenderer>().bounds.size.x / 2)));
    //       borderLines[4].transform.localScale = new Vector2(1+(0.004f * 2), 0.008f);
    //       borderLines[4].transform.name = "Bottom";
    //   }
}
