using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
    public SpriteRenderer rink;
    public SpriteRenderer innerRink;

    void Start()
    {
        double orthoSize = ((rink.bounds.size.x + (innerRink.bounds.size.x - rink.bounds.size.x))) * Screen.height / Screen.width * 0.52;
        Camera.main.orthographicSize = (float)orthoSize;
    }
}
