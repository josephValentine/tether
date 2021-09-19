using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public GameObject player;
    Vector3 offset;

    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void Update()
    {
        float newXPosition = player.transform.position.x - offset.x;
        float newYPosition = player.transform.position.y - offset.y;

        transform.position = new Vector3(newXPosition, newYPosition, transform.position.z);

    }
}
