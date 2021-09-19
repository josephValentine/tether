using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanceControllerz : MonoBehaviour
{
    private Vector3 moveDirection;
    public float speed = 30f;
    public PlayerController pc;
    // Start is called before the first frame update
    void Start()
    {
        moveDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        moveDirection.z = 0;
        moveDirection.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + moveDirection * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "lanceable")
        {
            Debug.Log("hit");
            speed = 0;
            GameObject player = GameObject.Find("Player");
            Debug.Log(player.GetComponent<PlayerController>());
            player.GetComponent<PlayerController>().setTethered(true, coll.gameObject);
            SpringJoint2D joint = player.AddComponent<SpringJoint2D>();
            joint.connectedBody = coll.gameObject.GetComponent<Rigidbody2D>();
            joint.dampingRatio = 1;
            //joint.distance = 4;
            //joint.spring = 0.1;
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("not lanceable");

        }
    }
}
