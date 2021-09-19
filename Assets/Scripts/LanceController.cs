using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanceController : MonoBehaviour
{
    private Vector3 moveDirection;
    public float speed = 60f;
    public PlayerController pc;
    private float life;
    public float maxLife = 100;
    // Start is called before the first frame update
    void Start()
    {
        moveDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        moveDirection.z = 0;
        moveDirection.Normalize();
        life = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + moveDirection * speed * Time.deltaTime;
        life += Time.deltaTime;
        if (life >= maxLife)
        {
            Destroy(gameObject);
        }

    }

    void OnTriggerEnter2D(Collider2D coll) {
        Debug.Log(coll);
        if (coll.gameObject.tag == "lanceable") {
            //speed = 0;
            GameObject player = GameObject.Find("Player");
            Debug.Log(player
                );
            if (!player.GetComponent<PlayerController>().isTouchingObjectTetheredTo())

            {
                Debug.Log("...not touching block");

                player.GetComponent<PlayerController>().setTethered(true, coll.gameObject);
                SpringJoint2D joint = player.AddComponent<SpringJoint2D>();
                joint.connectedBody = coll.gameObject.GetComponent<Rigidbody2D>();
                joint.dampingRatio = 0.7f;
                //joint.distance = 4;
                //joint.spring = 0.1f;
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("touching block...");

            }

        }
        else
        {
            Debug.Log("not lanceable");

        }
    }
}
